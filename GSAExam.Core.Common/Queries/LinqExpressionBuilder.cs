using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSAExam.Core.Common.Queries
{
    public class LinqExpressionBuilder
    {
        private static readonly IDictionary<string, string> _operatorMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { EntityFilterOperators.Equal, "==" },
            { EntityFilterOperators.NotEqual, "!=" },
            { EntityFilterOperators.LessThan, "<" },
            { EntityFilterOperators.LessThanOrEqual, "<=" },
            { EntityFilterOperators.GreaterThan, ">" },
            { EntityFilterOperators.GreaterThanOrEqual, ">=" }
        };
        private readonly StringBuilder _expression = new StringBuilder();
        private readonly List<object> _values = new List<object>();
        public IReadOnlyList<object> Parameters => _values;
        public string Expression => _expression.ToString();
        public void Build(EntityFilter entityFilter)
        {
            _expression.Length = 0;
            _values.Clear();
            if (entityFilter == null)
                return;
            Visit(entityFilter);
        }

        private void Visit(EntityFilter entityFilter)
        {
            WriteExpression(entityFilter);

            WriteGroup(entityFilter);
        }

        private void WriteExpression(EntityFilter entityFilter)
        {

            if (string.IsNullOrWhiteSpace(entityFilter.Name))
                return;
            var index = _values.Count;
            var name = entityFilter.Name;
            var value = entityFilter.Value;
            var o = string.IsNullOrWhiteSpace(entityFilter.Operator) ? "==" : entityFilter.Operator;
            _operatorMap.TryGetValue(o, out string comparison);
            if (string.IsNullOrEmpty(comparison))
                comparison = o.Trim();
            var negation = comparison.StartsWith("!") || comparison.StartsWith("not", StringComparison.OrdinalIgnoreCase) ? "!" : string.Empty;
            if (comparison.EndsWith(EntityFilterOperators.StartsWith, StringComparison.OrdinalIgnoreCase))
                _expression.Append($"{negation}{name}.StartsWith(@{index})");
            else if (comparison.EndsWith(EntityFilterOperators.EndsWith, StringComparison.OrdinalIgnoreCase))
                _expression.Append($"{negation}{name}.EndsWith(@{index})");
            else if (comparison.EndsWith(EntityFilterOperators.Contains, StringComparison.OrdinalIgnoreCase))
                _expression.Append($"{negation}{name}.Contains(@{index})");
            else
                _expression.Append($"{name} {comparison} @{index}");
            _values.Add(value);
        }

        private bool WriteGroup(EntityFilter entityFilter)
        {
            var hasGroup = entityFilter.Filters != null && entityFilter.Filters.Any();
            if (!hasGroup)
                return false;

            if (!string.IsNullOrWhiteSpace(entityFilter.Name))
            {
                var logic = string.IsNullOrWhiteSpace(entityFilter.Logic)
                    ? EntityFilterLogic.And
                    : entityFilter.Logic;
                _expression.Append($" {logic} (");
            }
            else
                _expression.Append("(");

            foreach (var filter in entityFilter.Filters)
            {
                Visit(filter);

                var isLast = entityFilter.Filters.LastOrDefault() == filter;
                if (filter == null) continue;
                var filterLogic = string.IsNullOrWhiteSpace(filter.Logic)
                    ? EntityFilterLogic.And
                    : filter.Logic;
                if (!isLast)
                    _expression.Append($" {filterLogic} ");
            }
            _expression.Append(")");
            return true;
        }
    }
}
