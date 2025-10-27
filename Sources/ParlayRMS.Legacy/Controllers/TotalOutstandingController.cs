namespace ParlayRMS.Legacy.Controllers;

using Fanex.NAP.Client.Attributes;
using Fanex.NAP.Client.Helpers.Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using ParlayRMS.Models.Functions;
using Services;
using ViewModel.TotalOutstanding;

public class TotalOutstandingController(
    TotalOutstandingService _service,
    MembersService _membersService) : BaseController
{
    [HttpGet]
    [NapAuth(FunctionKey = FunctionKeys.TotalOutstanding, Policy = Constant.Policy.Read)]
    public ActionResult Index(IndexModel model)
    {
        var combos = new List<SelectListItem>();
        for (var i = 2; i <= 9; i++)
        {
            combos.Add(new SelectListItem
            {
                Text = i.ToString(),
                Value = i.ToString(),
                Selected = string.IsNullOrEmpty(model.Combo) || model.Combo.Contains(i.ToString())
            });
        }

        combos.Add(new SelectListItem { Text = ViewLang.TenUp, Value = "10", Selected = string.IsNullOrEmpty(model.Combo) || model.Combo.Contains("10") });
        ViewBag.Combos = combos;

        var parlayTypes = new List<SelectListItem>
        {
            new SelectListItem
            {
                Text = ViewLang.MixparLay,
                Value = ((int)ParlayType.MixParlay).ToString(),
                Selected = string.IsNullOrEmpty(model.ParlayTypes) || model.ParlayTypes.Contains(((int)ParlayType.MixParlay).ToString())
            },
            new SelectListItem
            {
                Text = ViewLang.ParlayBoost,
                Value = ((int)ParlayType.ParlayBoost).ToString(),
                Selected = string.IsNullOrEmpty(model.ParlayTypes) || model.ParlayTypes.Contains(((int)ParlayType.ParlayBoost).ToString())
            },
            new SelectListItem
            {
                Text = ViewLang.ParlayGenerator,
                Value = ((int)ParlayType.ParlayGenerator).ToString(),
                Selected = string.IsNullOrEmpty(model.ParlayTypes) || model.ParlayTypes.Contains(((int)ParlayType.ParlayGenerator).ToString())
            },
            new SelectListItem
            {
                Text = ViewLang.ComboParlay,
                Value = ((int)ParlayType.ComboParlay).ToString(),
                Selected = string.IsNullOrEmpty(model.ParlayTypes) || model.ParlayTypes.Contains(((int)ParlayType.ComboParlay).ToString())
            },
            new SelectListItem
            {
                Text = ViewLang.PenaltyShootoutCombo,
                Value = ((int)ParlayType.PenaltyShootoutCombo).ToString(),
                Selected = string.IsNullOrEmpty(model.ParlayTypes) || model.ParlayTypes.Contains(((int)ParlayType.PenaltyShootoutCombo).ToString())
            },
            new SelectListItem
            {
                Text = ViewLang.SingleMatchParlay,
                Value = ((int)ParlayType.SingleMatchParlay).ToString(),
                Selected = string.IsNullOrEmpty(model.ParlayTypes) || model.ParlayTypes.Contains(((int)ParlayType.SingleMatchParlay).ToString())
            },
            new SelectListItem
            {
                Text = ViewLang.HybridParlay,
                Value = ((int)ParlayType.HybridParlay).ToString(),
                Selected = string.IsNullOrEmpty(model.ParlayTypes) || model.ParlayTypes.Contains(((int)ParlayType.HybridParlay).ToString())
            }
        };

        ViewBag.ParlayTypes = parlayTypes;

        return View("Index", model);
    }

    [HttpPost]
    public ActionResult Data(DataModel model)
    {
        ViewBag.Username = model.Username;
        var customerInfo = _membersService.GetCustomerInfo(model.Username);
        ViewBag.CustId = customerInfo?.CustId;
        if (customerInfo == null || customerInfo.CustId == 0)
        {
            return PartialView("_ErrorMessage", $"{model.Username} is not existed");
        }

        if (customerInfo.RoleId == (int)RoleId.Member)
        {
            var data = _service.GetTotalOutstandingForMember(customerInfo.CustId, model.Combo, model.SportsType, model.ParlaysType, model.IsCornerParlay);
            ViewBag.CustomerClass = string.Empty;
            ViewBag.TotalData = null;
            if (data.Count > 0)
            {
                decimal totalStackPt = 0;
                decimal totalMaxPayoutPt = 0;
                decimal totalPotentialWinningAmount = 0;
                foreach (var item in data)
                {
                    totalStackPt += item.StakePt;
                    totalMaxPayoutPt += item.MaxPayoutPt;
                    totalPotentialWinningAmount += item.PotentialWinningAmount;
                }
                ViewBag.CustomerClass = data.FirstOrDefault().CustomerClass;
                ViewBag.TotalData = new TotalDataModel
                {
                    StakePt = totalStackPt,
                    PotentialWinningAmount = totalPotentialWinningAmount,
                    MaxPayoutPt = totalMaxPayoutPt
                };
            }

            return PartialView("MemberResult", data);
        }

        if (customerInfo.RoleId == (int)RoleId.Agent)
        {
            var data = _service.GetTotalOutstandingForAgent(customerInfo.CustId, model.Combo, model.SportsType, model.ParlaysType, model.IsCornerParlay);
            ViewBag.TotalData = null;
            ViewBag.CustomerClass = string.Empty;
            if (data.Count > 0)
            {
                decimal totalStackPt = 0;
                decimal totalMaxPayoutPt = 0;
                var totalTotalBets = 0;
                foreach (var item in data)
                {
                    totalStackPt += item.StakePt;
                    totalMaxPayoutPt += item.MaxPayoutPt;
                    totalTotalBets += item.BetCount;
                }

                ViewBag.TotalData = new TotalDataModel
                {
                    StakePt = totalStackPt,
                    MaxPayoutPt = totalMaxPayoutPt,
                    TotalBets = totalTotalBets
                };
            }

            return PartialView("AgentResult", data);
        }
        var roleName = customerInfo.RoleId == (int)RoleId.Master ? "master" : "super";

        return PartialView("_ErrorMessage",
            $"{model.Username} is a {roleName}. Please input a member or agent username");
    }
}