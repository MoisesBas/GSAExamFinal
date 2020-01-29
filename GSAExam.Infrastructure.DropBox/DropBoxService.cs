using AutoMapper;
using GSAExam.Core.Domain.Entities;
using GSAExam.Core.DomainModel.Student;
using GSAExam.Infrastructure.Interface;
using Microsoft.Extensions.DependencyInjection;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GSAExam.Infrastructure.DropBox
{
    public class DropBoxService:IDropBoxService
    {
        private readonly HttpClient _httpClientDropBox;       
      
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper Mapper;
        public DropBoxService(IServiceScopeFactory serviceScopeFactory,IMapper mapper, HttpClient httpClientDropBox )
        {
            _scopeFactory = serviceScopeFactory;
            Mapper = mapper;
            _httpClientDropBox = httpClientDropBox;           
        }

        public async Task<bool> Process(string path)
        {
            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<IGSADbContext>();
            var response = await _httpClientDropBox.GetAsync(path);
            using (Stream stream = await response.Content.ReadAsStreamAsync())
            {
                using ZipArchive zip = new ZipArchive(stream);
                var entry = zip.Entries.LastOrDefault();
                if (entry != null)
                {
                    Stream sr = entry.Open();
                    using ExcelPackage package = new ExcelPackage(sr);
                    var worksheet = package.Workbook.Worksheets["Sheet1"];
                    var result = GetStudentInsertUpdate(worksheet);
                    if (result.Any())
                    {
                        
                             var dbSet = db.Set<Students>();
                        var students = result.Select(x => x.FirstName.Trim().ToUpper()).ToArray();
                        var exists = dbSet.Where(x => students.Contains(x.FirstName.Trim().ToUpper())).ToList();
                        if (!exists.Any())
                        {                          
                                result.ForEach(x =>
                                {   var map = Mapper.Map<Students>(x);
                                    dbSet.AddRange(map);
                                });                        
                        }
                        else
                        {
                            var names = exists.Select(x => x.FirstName.Trim().ToUpper()).ToArray();
                            var items = result.Where(x => !names.Contains(x.FirstName.Trim().ToUpper())).ToList();
                            if (items.Any())
                            {
                                items.ForEach(x =>
                                {
                                    var map = Mapper.Map<Students>(x);
                                    dbSet.AddRange(map);
                                });

                            }
                        }
                        await db.SaveChangesAsync(default).ConfigureAwait(false);
                    }

                    return true;

                }

            }
            return false;

        }

        private StudentUpdateCreateModel CreateStudent(ExcelWorksheet sheet, int rowIterator)
        {
            var email = sheet.Cells[rowIterator, 1].Value == null ? string.Empty : sheet.Cells[rowIterator, 1].Value.ToString();
            var fullname = sheet.Cells[rowIterator, 2].Value == null ? string.Empty : sheet.Cells[rowIterator, 2].Value.ToString();
            var status = sheet.Cells[rowIterator, 3].Value == null ? string.Empty : sheet.Cells[rowIterator, 3].Value.ToString();
            var dates = sheet.Cells[rowIterator, 4].Value == null ? string.Empty : sheet.Cells[rowIterator, 4].Value.ToString();
            var name = fullname.Split(new char[0]);
            return new StudentUpdateCreateModel()
            {
                FirstName = name.Any() ? name[0].ToString() : string.Empty,
                LastName = name.Any() ? name[1].ToString() : string.Empty,
                Email = email,
                Dates = GetDate(dates),
                Status = status
            };
        }
        private DateTime? GetDate(string date)
        {
            return string.IsNullOrEmpty(date) ? (DateTime?)null : DateTime.Parse(date);


        }
        private List<StudentUpdateCreateModel> GetStudentInsertUpdate(ExcelWorksheet sheet)
        {
            var result = new List<StudentUpdateCreateModel>();
            var noOfCol = sheet.Dimension.End.Column;
            var noOfRow = sheet.Dimension.End.Row;

            for (var rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
            {
                result.Add(CreateStudent(sheet, rowIterator));
            }



            return result;
        }
    }
}
