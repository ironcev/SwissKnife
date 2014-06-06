using System.Collections.Generic;
using MvcContrib.Pagination;

namespace SwissKnife.Experiments.QueryStringAndIEnumerablePropertiesInAspDotNetMvc.Models
{
    public class ExampleViewModel
    {
        public IEnumerable<string> StringItems { get; set; }
        public IEnumerable<string> SelectedStringItems { get; set; }

        public IEnumerable<int> IntegerItems { get; set; }
        public IEnumerable<int> SelectedIntegerItems { get; set; }

        public IPagination<GridItemViewModel> SearchResult { get; set; }
    }

    public class GridItemViewModel
    {
        public string String { get; set; }
        public int Integer { get; set; }
    }
}