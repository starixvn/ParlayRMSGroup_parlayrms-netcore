namespace ParlayRMS.Legacy.Controllers;

using Fanex.NAP.Client.Attributes;
using Fanex.NAP.Client.Helpers.Business;
using Microsoft.AspNetCore.Mvc;
using ParlayRMS.Models.Functions;
using Services;
using ViewModel.Monitoring;

public class PotentialWinningController(
    PotentialWinningMonitorService _potentialWinning,
    CommonService _commonService
    ) : BaseController
{
    [NapAuth(FunctionKey = FunctionKeys.PotentialWinningMonitor, Policy = Constant.Policy.Read)]
    [HttpGet]
    public ActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public ActionResult GetData(PotentialWinningModel model)
    {
        ModelState.Remove("CustomerClass");
        if (!ModelState.IsValid)
        {
            return InvalidModelJson();
        }

        var data = _potentialWinning.GetPotentialWinningList(model.Combos, model.ParlayTypes, model.SportsTypes, model.Levels, model.WinningAmount, model.SelectTop, model.IsCornerParlay, model.FilterBy, model.AgentId, model.CustomerClass, model.Company, model.UserName);

        return Json(new
        {
            Code = 0,
            Records = data
        });
    }

    [HttpPost]
    public ActionResult ViewDetailList(PotentialWinningDetailModel detailModel)
    {
        var model = _potentialWinning.GetPotentialWinningDetailList(detailModel.Combos, detailModel.ParlayTypes, detailModel.SportsTypes, detailModel.CustId, detailModel.IsCornerParlay);
        ViewBag.CustId = detailModel.CustId;
        ViewBag.BetDetailUserName = detailModel.UserName;
        ViewBag.IsDislayCheckedBy = !detailModel.AgentId.HasValue;
        return PartialView(model);
    }

    [HttpPost]
    public ActionResult CheckBy(UserTrackingCriteria criteria)
    {
        var data = _potentialWinning.UpdateCheckedUser(criteria.UserId, criteria.Username, criteria.TransId);
        return Json(data);
    }

    [HttpPost]
    public ActionResult Company()
    {
        var data = _commonService.GetCompanies();
        return Json(data.GroupBy(x => x.IsDeposit).Select(y => new { GroupId = y.Key, Items = y.Select(z => z.Site) }).ToList());
    }
}