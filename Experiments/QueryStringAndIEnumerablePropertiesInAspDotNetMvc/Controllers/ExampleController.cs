using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using MvcContrib.Pagination;
using SwissKnife.Experiments.QueryStringAndIEnumerablePropertiesInAspDotNetMvc.Models;

namespace SwissKnife.Experiments.QueryStringAndIEnumerablePropertiesInAspDotNetMvc.Controllers
{
    public class ExampleController : Controller
    {
        private static readonly string[] stringItems = { "A", "B", "C", "D", "E", "F", "G", "A,B", "C,D" };
        private static readonly int[] integerItems = { 1, 2, 3, 4, 5, 6, 7 };
        private static readonly List<GridItemViewModel> allGridItems = new List<GridItemViewModel>();

        static ExampleController()
        {
            foreach(string stringValue in stringItems)
                foreach (int integerValue in integerItems)
                    allGridItems.Add(new GridItemViewModel {String = stringValue, Integer = integerValue});
        }

        public ActionResult Get([DefaultValue(new string[0])] string[] selectedStringItems, [DefaultValue(new int[0])] int[] selectedIntegerItems, [DefaultValue(1)] int page)
        {
            const int pageSize = 5;

            var viewModel = new ExampleViewModel();
            viewModel.StringItems = stringItems;
            viewModel.SelectedStringItems = selectedStringItems;

            viewModel.IntegerItems = integerItems;
            viewModel.SelectedIntegerItems = selectedIntegerItems;

            var searchResult = allGridItems.Where(item => (selectedIntegerItems.Contains(item.Integer) || selectedIntegerItems.Length <= 0) &&
                                                          (selectedStringItems.Contains(item.String) || selectedStringItems.Length <= 0)).ToArray();

            var searchResultSinglePage = searchResult.Skip((page - 1)*pageSize).Take(pageSize);

            viewModel.SearchResult = new CustomPagination<GridItemViewModel>(searchResultSinglePage, page, pageSize, searchResult.Length);

            return View("Page", viewModel);
        }
	}
}