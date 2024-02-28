LeagueBetTypeAnalysis = {
    DOM: {
        houseWinLossDiv: "#houseWinLossDiv",
        reportByDiv: "#reportByDiv",
        ratioDiv: "#ratioDiv",
        betTypeDiv: "#betTypeDiv",
        leagueDiv: "#leagueDiv"
    },
    cachedObject: {},
    LeagueList: [],
    totalLeagueGroup: 0,
    Init: function () {
        LeagueBetTypeAnalysis.cachedObject = {
            parlayType: null,
            fromDate: null,
            toDate: null,
            leagueGroups: null,
            leagues: null,
            site: null,
            ratio: null,
            reportBy: null,
            sportType: null,
            betType: null,
            houseWL: null,
            sortById: null,
            isSortAsc: null,
            page: 1,
            pageSize: 100
        };
        var nowDate = moment(dateRanges.NowDate).format('MMMM DD, YYYY');
        var minDate = moment(dateRanges.LimitDataFromDate).format('MMMM DD, YYYY');

        LeagueBetTypeAnalysis.HandleElementForLeagueBetTypeTab();

        $("#tabs").tabs({
            activate: function (event, ui) {
                var element = ui.newPanel.selector;
                switch (element) {
                    case '#tabs-league-bettype':
                        LeagueBetTypeAnalysis.HandleElementForLeagueBetTypeTab();
                        break;
                    case '#tabs-league':
                        LeagueBetTypeAnalysis.HandleElementForLeagueTab(-1);
                        break;
                    default:
                        LeagueBetTypeAnalysis.HandleElementForBetTypeTab('');
                    // code block
                }
            },
            beforeActivate: function (event, ui) {
                Validators.hide();
            }
        });
        $("#tabs").removeClass("ui-widget");

        $("#ddlParlayType").multiselect({
            buttonWidth: 165,
            menuHeight: 155,
            create: function () {
                $("#ddlParlayType").multiselect("checkAll");
            },
            selectedText: function (numChecked, numTotal, checkedItems) {
                if (numChecked == 0) {
                    return "Select options";
                }
                else if (numChecked == 1) {
                    return $(checkedItems).attr('title');
                }
                else if (numChecked == numTotal) {
                    return "All";
                }
                else {
                    return numChecked + ' options';
                }
            }
        });

        $("#ddlGroup").multiselect({
            buttonWidth: 176,
            menuHeight: 250,
            selectedList: 1,
            create: function () {
                $("#ddlGroup").multiselect();
            },
            checkAll: function () { LeagueBetTypeAnalysis.HandleLeagueControl(1) },
            selectedText: function (numChecked, numTotal, checkedItems) {
                if (numChecked == 0) {
                    return "Select options";
                }
                else if (numChecked == 1) {
                    return checkedItems[0].labels[0].innerText;
                }
                else if (numChecked == numTotal) {
                    return "All";
                }
                else {
                    return numChecked + ' options';
                }
            }
        });

        $("#ddlBetType").multiselect({
            menuHeight: 300,
            buttonWidth: 180,
            groupsCollapsable: true,
            create: function () {
                $("#ddlBetType").multiselect("checkAll");
            },
            selectedText: function (numChecked, numTotal, checkedItems) {
                if (numChecked == 0) {
                    return "Select options";
                }
                else if (numChecked == 1) {
                    return checkedItems[0].labels[0].innerText;
                }
                else if (numChecked == numTotal) {
                    return "All";
                }
                else {
                    return numChecked + ' options';
                }
            }
        }).multiselectfilter({
            autoReset: true
        });

        $("#ddlLeague").multiselect({
            beforeopen: function () { $(this).multiselect('widget').width(200); },
            buttonWidth: 200,
            menuHeight: 200,
            create: function () {
                $("#ddlLeague").multiselect("checkAll");
            },
            checkAll: function () { LeagueBetTypeAnalysis.HandleLeagueControl(2) },
            selectedText: function (numChecked, numTotal, checkedItems) {
                if (numChecked == 0) {
                    return "Select options";
                }
                else if (numChecked == 1) {
                    var selectedText = checkedItems[0].labels[0].innerText;
                    var characters = parseInt($("#ddlSport").val()) === 1 ? 20 : 24;
                    return selectedText.length > characters ? selectedText.substring(0, characters) + ' ...' : selectedText;
                }
                else if (numChecked == numTotal) {
                    return "All";
                }
                else {
                    return numChecked + ' options';
                }
            }
        }).multiselectfilter({
            autoReset: true
        });

        $("#ddlSite").multiselect({
            buttonWidth: 80,
            menuHeight: 100,
            header: false,
            multiple: false,
            selectedList: 1
        });

        $("#ddlSport").multiselect({
            buttonWidth: 90,
            menuHeight: 80,
            header: false,
            multiple: false,
            selectedList: 1
        });

        $("#fromDate").datepicker({
            dateFormat: "MM dd, yy",
            showButtonPanel: true,
            changeMonth: true,
            changeYear: true,
            minDate: minDate,
            maxDate: nowDate,
            onSelect: function (dateText, inst) {
                if (dateText != inst.lastVal) {
                    $("#dateRange option[value='7']").attr('selected', 1);
                    $("#dateRange").multiselect("refresh");
                }
            },
            onClose: function (selectedDate) {
                $("#toDate").datepicker("option", "minDate", selectedDate);
            }
        });

        $("#toDate").datepicker({
            dateFormat: "MM dd, yy",
            showButtonPanel: true,
            changeMonth: true,
            changeYear: true,
            minDate: minDate,
            maxDate: nowDate,
            onSelect: function (dateText, inst) {
                if (dateText != inst.lastVal) {
                    $("#dateRange option[value='7']").attr('selected', 1);
                    $("#dateRange").multiselect("refresh");
                }
            },
            onClose: function (selectedDate) {
                $("#fromDate").datepicker("option", "maxDate", selectedDate);
            }
        });

        var nowDate = moment(dateRanges.NowDate);
        $("#fromDate").val(nowDate.format('MMMM DD, YYYY'));
        $("#toDate").val(nowDate.format('MMMM DD, YYYY'));
        $("#txtHouseWL").autoNumeric({
            aSep: ',',
            aDec: '.',
            vMin: '-99999999999',
            vMax: '99999999999',
            aPad: false,
            aForm: false
        });
        $("#btnSearch").addClass("button");
        $('#btnSearch').click(function () {
            Validators.hide();
            $("#divResult").html('');
            LeagueBetTypeAnalysis.ResetCachedObject();
            var fromDate = $("#fromDate").val();
            var toDate = $("#toDate").val();

            if (validator.validateDate("#fromDate")) {
                return;
            }

            if (validator.validateDate("#toDate")) {
                return;
            }

            var parlayType = $("#ddlParlayType").multiselect("getChecked").map(function () {
                return this.value;
            }).get().join(',');
            if (parlayType == '') {
                Validators.show($("#ddlParlayType").multiselect("getButton").get(0), 'Please select your options');
                return;
            }

            var leagueGroups = $("#ddlGroup").multiselect("getChecked").map(function () {
                return this.value;
            }).get().join(',');
            if (leagueGroups == '') {
                Validators.show($("#ddlGroup").multiselect("getButton").get(0), 'Please select your options');
                return;
            }

            var leagues = '';
            if (leagueGroups == '-1') {
                leagues = '-1';
            } else {
                leagues = $("#ddlLeague").multiselect("getChecked").map(function () {
                    return this.value;
                }).get().join(',');
                if ($("#ddlLeague").multiselect("getButton").get(0).innerText == 'All' && leagueGroups.search('-1') > -1) {
                    leagues = '-1,' + leagues;
                }
            }

            if (leagues == '') {
                Validators.show($("#ddlLeague").multiselect("getButton").get(0), 'Please select your options');
                return;
            }

            var reportBy = $('input[name=rdlReportBy]:checked').val();

            var sportType = parseInt($("#ddlSport").val());

            if (leagueGroups == '-1' && reportBy == '2') {
                Validators.show($("#ddlGroup").multiselect("getButton").get(0), 'Please select another option to get correct report!');
            }

            if (leagues.split(',').length > 1 && reportBy == '2' && $("#ddlGroup").multiselect("getButton").get(0).innerText != 'All') {
                Validators.show($("#ddlLeague").multiselect("getButton").get(0), 'Please select single League to get correct report!');
            }

            var site = parseInt($("#ddlSite").val());

            var ratio = isNaN(parseInt($("#txtRatio").val())) ? 0 : parseFloat($("#txtRatio").val()) / 100;

            if (ratio > 1) {
                Validators.show($("#txtRatio"), 'Invalid Ratio');
                return;
            }

            $('#header').find(".sortable").each(function () {
                $(this).removeClass("headerSortDown").removeClass("headerSortUp");
                this.sortDirection = "";
            });

            var betType = '';
            var houseWL = 0;

            if ($("#tabs").tabs('option', 'active') == 1) {
                reportBy = 3;
                houseWL = $.trim($('#txtHouseWL').val().replace(/,/g, ''));
            }

            if ($("#tabs").tabs('option', 'active') == 2) {
                reportBy = 4;
                betType = $("#ddlBetType").multiselect("getChecked").map(function () {
                    return this.value;
                }).get().join(',');
            }

            LeagueBetTypeAnalysis.Data(parlayType,
                fromDate,
                toDate,
                leagueGroups,
                leagues,
                site,
                ratio,
                reportBy,
                sportType,
                betType,
                houseWL,
                LeagueBetTypeAnalysis.cachedObject.sortById,
                LeagueBetTypeAnalysis.cachedObject.isSortAsc,
                LeagueBetTypeAnalysis.cachedObject.page,
                LeagueBetTypeAnalysis.cachedObject.pageSize);
        });

        $('#ddlGroup').on('change', function () {
            var leagueGroups = $("#ddlGroup").multiselect("getChecked").map(function () {
                return this.value;
            }).get().join(',');
            var leagues = $("#ddlLeague").multiselect("getChecked").map(function () {
                return this.value;
            }).get().join(',');
            if (LeagueBetTypeAnalysis.IsDisabled($('input[name=rdlReportBy]:checked').val(), leagueGroups, leagues)) {
                $("#ddlLeague").multiselect("disable");
                if (leagueGroups == '-1') {
                    var html = leagueGroups == '' ? '' : '<option value="0" aria-selected="true">All</option>';
                    $('#ddlLeague').html(html);
                    $('#ddlLeague').multiselect('refresh');
                }
            } else {
                $("#ddlLeague").multiselect("enable");
                LeagueBetTypeAnalysis.ReloadLeagues(leagueGroups);
            }
        });

        $('input[name=rdlReportBy]').on('change', function () {
            var leagueGroups = $("#ddlGroup").multiselect("getChecked").map(function () {
                return this.value;
            }).get().join(',');
            var leagues = $("#ddlLeague").multiselect("getChecked").map(function () {
                return this.value;
            }).get().join(',');
            if (LeagueBetTypeAnalysis.IsDisabled(this.value, leagueGroups, leagues)) {
                $("#ddlLeague").multiselect("disable");
                if (leagueGroups.split(',').length == LeagueBetTypeAnalysis.totalLeagueGroup) {
                    LeagueBetTypeAnalysis.ReloadLeagues(leagueGroups);
                }
            } else {
                $("#ddlLeague").multiselect("enable");
            }
        });

        LeagueBetTypeAnalysis.GroupsAndLeagues(1, $("#fromDate").val(), $("#toDate").val());

        LeagueBetTypeAnalysis.BetTypes(1);

        $('#ddlSport').on('change', function () {
            LeagueBetTypeAnalysis.GroupsAndLeagues(parseInt($(this).val()), $("#fromDate").val(), $("#toDate").val());
            LeagueBetTypeAnalysis.BetTypes(parseInt($(this).val()));
        });

        $(document).on("click", '#header .sortable', function () {
            var currentObject = this;
            var currentColIndex = parseInt($(currentObject).attr("data-col-index"));

            if (currentColIndex == LeagueBetTypeAnalysis.cachedObject.sortById) {
                if (LeagueBetTypeAnalysis.cachedObject.isSortAsc) {
                    LeagueBetTypeAnalysis.cachedObject.isSortAsc = false;
                    $(this).removeClass("headerSortUp").addClass("headerSortDown");
                } else {
                    LeagueBetTypeAnalysis.cachedObject.isSortAsc = true;
                    $(this).removeClass("headerSortDown").addClass("headerSortUp");
                }
            } else {
                if (currentObject.sortDirection == 'asc') {
                    LeagueBetTypeAnalysis.cachedObject.isSortAsc = true;
                    $(this).removeClass("headerSortDown").addClass("headerSortUp");
                } else {
                    LeagueBetTypeAnalysis.cachedObject.isSortAsc = false;
                    $(this).removeClass("headerSortUp").addClass("headerSortDown");
                }

                $('#header').find(".sortable").each(function () {
                    if ($(currentObject).index() != $(this).index()) {
                        $(this).removeClass("headerSortDown").removeClass("headerSortUp");
                        this.sortDirection = "";
                    }
                });
            }

            if (!currentColIndex) {
                currentColIndex = $(currentObject).index();
            }

            LeagueBetTypeAnalysis.cachedObject.sortById = parseInt(currentColIndex);
            LeagueBetTypeAnalysis.Data(LeagueBetTypeAnalysis.cachedObject.parlayType,
                LeagueBetTypeAnalysis.cachedObject.fromDate,
                LeagueBetTypeAnalysis.cachedObject.toDate,
                LeagueBetTypeAnalysis.cachedObject.leagueGroups,
                LeagueBetTypeAnalysis.cachedObject.leagues,
                LeagueBetTypeAnalysis.cachedObject.site,
                LeagueBetTypeAnalysis.cachedObject.ratio,
                LeagueBetTypeAnalysis.cachedObject.reportBy,
                LeagueBetTypeAnalysis.cachedObject.sportType,
                LeagueBetTypeAnalysis.cachedObject.betType,
                LeagueBetTypeAnalysis.cachedObject.houseWL,
                LeagueBetTypeAnalysis.cachedObject.sortById,
                LeagueBetTypeAnalysis.cachedObject.isSortAsc,
                LeagueBetTypeAnalysis.cachedObject.page,
                LeagueBetTypeAnalysis.cachedObject.pageSize);
        });

        $(document).on('click', '.leagueTab', function () {
            $("#tabs").tabs("option", "active", 1);
            var leagueId = this.getAttribute('data-leagueId');
            LeagueBetTypeAnalysis.HandleElementForLeagueTab(leagueId);
            LeagueBetTypeAnalysis.Data(LeagueBetTypeAnalysis.cachedObject.parlayType,
                LeagueBetTypeAnalysis.cachedObject.fromDate,
                LeagueBetTypeAnalysis.cachedObject.toDate,
                LeagueBetTypeAnalysis.cachedObject.leagueGroups,
                leagueId,
                LeagueBetTypeAnalysis.cachedObject.site,
                LeagueBetTypeAnalysis.cachedObject.ratio,
                3,
                LeagueBetTypeAnalysis.cachedObject.sportType,
                '',
                -50000,
                null,
                null,
                1,
                100);
        });

        $(document).on('click', '.betTypeTab', function () {
            $("#tabs").tabs("option", "active", 2);
            var bettype = this.getAttribute('data-bettypeId');
            LeagueBetTypeAnalysis.HandleElementForBetTypeTab(bettype);
            LeagueBetTypeAnalysis.Data(LeagueBetTypeAnalysis.cachedObject.parlayType,
                LeagueBetTypeAnalysis.cachedObject.fromDate,
                LeagueBetTypeAnalysis.cachedObject.toDate,
                LeagueBetTypeAnalysis.cachedObject.leagueGroups,
                LeagueBetTypeAnalysis.cachedObject.leagues,
                LeagueBetTypeAnalysis.cachedObject.site,
                LeagueBetTypeAnalysis.cachedObject.ratio,
                4,
                LeagueBetTypeAnalysis.cachedObject.sportType,
                bettype,
                -50000,
                null,
                null,
                1,
                100);
        });

        $(document).on('click', '.parlayWonTicket', function () {
            var title = $("#tabs").tabs('option', 'active') === 1 ?
                "Group [" + $(this).data('group') + "] - League [" + $(this).data('league') + "] - Event [" + $(this).data('event') + "] - [" + $(this).data('bettype') + "] - [" + $(this).data('betchoice') + "]"
                : "Group [" + $(this).data('group') + "] - League [" + $(this).data('league') + "] - Event [" + $(this).data('event') + "] - [" + $(this).data('bettype') + "]";
            LeagueBetTypeAnalysis.ExportExcelForWonTickets($(this).data('translist'), title);
        });
    },

    GroupsAndLeagues: function (sportType, fromDate, toDate) {
        var data = {
            sportType: sportType,
            fromDate: fromDate,
            toDate: toDate
        }

        $.ajax({
            url: resolveClientUrl('LeagueBetTypeAnalysis/GroupsAndLeagues'),
            type: 'POST',
            data: data,
            dataType: 'html',
            cache: false,
            beforeSend: function (xhr) {
                loading.show();
            },
            success: function (result) {
                var json = $.parseJSON(result);
                LeagueBetTypeAnalysis.ReloadLeagueGroups(json.Groups);
                LeagueBetTypeAnalysis.totalLeagueGroup = json.Groups.length;
                LeagueBetTypeAnalysis.LeagueList = json.Leagues;
                var leagueGroups = $("#ddlGroup").multiselect("getChecked").map(function () {
                    return this.value;
                }).get().join(',');
                LeagueBetTypeAnalysis.ReloadLeagues(leagueGroups);
            },
            error: function (xhr, status, err) {
                alertBox.showError(xhr);
            },
            complete: function () {
                loading.hide();
            }
        });
    },

    BetTypes: function (sportType) {
        var data = {
            sportType: sportType
        }

        $.ajax({
            url: resolveClientUrl('LeagueBetTypeAnalysis/GetBettypes'),
            type: 'POST',
            data: data,
            dataType: 'html',
            cache: false,
            beforeSend: function (xhr) {
                loading.show();
            },
            success: function (result) {
                var json = $.parseJSON(result);
                LeagueBetTypeAnalysis.LoadBettypeControl(json);
            },
            error: function (xhr, status, err) {
                alertBox.showError(xhr);
            },
            complete: function () {
                loading.hide();
            }
        });
    },

    Data: function (parlayType, fromDate, toDate, leagueGroups, leagues, site, ratio, reportBy, sportType, betType, houseWL, sortById, isSortAsc, page, pageSize) {
        var data = {
            fromDate: fromDate,
            toDate: toDate,
            parlayType: parlayType,
            leagueGroups: leagueGroups,
            leagues: leagues,
            site: site,
            ratio: ratio,
            reportBy: reportBy,
            sportType: sportType,
            betType: betType,
            houseWL: houseWL,
            page: page,
            sortById: sortById,
            isSortAsc: isSortAsc,
            pageSize: pageSize
        }

        LeagueBetTypeAnalysis.cachedObject = data;

        $.ajax({
            url: resolveClientUrl('LeagueBetTypeAnalysis/Data'),
            type: 'POST',
            data: data,
            dataType: 'html',
            cache: false,
            beforeSend: function (xhr) {
                loading.show();
            },
            success: function (result) {
                $("#divResult").html(result);
                $('#header').find(".sortable").each(function () {
                    if ($(this).attr("data-col-index") == LeagueBetTypeAnalysis.cachedObject.sortById) {
                        if (LeagueBetTypeAnalysis.cachedObject.isSortAsc) {
                            $(this).removeClass("headerSortDown").addClass("headerSortUp");
                        } else {
                            $(this).removeClass("headerSortUp").addClass("headerSortDown");
                        }
                    }
                });
                $('#pageSize').on('change', function () {
                    LeagueBetTypeAnalysis.cachedObject.pageSize = parseInt($(this).val());
                    LeagueBetTypeAnalysis.cachedObject.page = 1;
                    LeagueBetTypeAnalysis.Data(LeagueBetTypeAnalysis.cachedObject.parlayType,
                        LeagueBetTypeAnalysis.cachedObject.fromDate,
                        LeagueBetTypeAnalysis.cachedObject.toDate,
                        LeagueBetTypeAnalysis.cachedObject.leagueGroups,
                        LeagueBetTypeAnalysis.cachedObject.leagues,
                        LeagueBetTypeAnalysis.cachedObject.site,
                        LeagueBetTypeAnalysis.cachedObject.ratio,
                        LeagueBetTypeAnalysis.cachedObject.reportBy,
                        LeagueBetTypeAnalysis.cachedObject.sportType,
                        LeagueBetTypeAnalysis.cachedObject.betType,
                        LeagueBetTypeAnalysis.cachedObject.houseWL,
                        LeagueBetTypeAnalysis.cachedObject.sortById,
                        LeagueBetTypeAnalysis.cachedObject.isSortAsc,
                        LeagueBetTypeAnalysis.cachedObject.page,
                        LeagueBetTypeAnalysis.cachedObject.pageSize);
                });
            },
            error: function (xhr, status, err) {
                alertBox.showError(xhr);
            },
            complete: function () {
                loading.hide();
            }
        });
    },
    ExportExcel: function (parlayType, fromDate, toDate, leagues, leagueGroups, site, ratio, reportBy, sportType, betType, houseWL) {

        var params = 'fromDate=' + fromDate + '&toDate=' + toDate + '&parlayType=' + parlayType + '&leagueGroups=' + leagueGroups
            + '&leagues=' + leagues + '&site=' + site + '&ratio=' + ratio + '&reportBy=' + reportBy + '&sportType=' + sportType
            + '&betType=' + betType + '&houseWL=' + houseWL;

        var url = '';

        switch (reportBy) {
            case '1':
                url = 'LeagueBetTypeAnalysis/ExportExcelForLeague';
                break;
            case '2':
                url = 'LeagueBetTypeAnalysis/ExportExcelForBetType';
                break;
            case '3':
                url = 'LeagueBetTypeAnalysis/ExportExcelForLeagueDetails';
                break;
            default:
                url = 'LeagueBetTypeAnalysis/ExportExcelForBetTypeDetails';
                break;
        }

        var xhttp = new XMLHttpRequest();
        xhttp.onreadystatechange = function () {
            var element;
            if (xhttp.readyState === 4 && xhttp.status === 200) {
                element = document.createElement('a');
                element.href = window.URL.createObjectURL(xhttp.response);
                element.download = "LeagueBetTypeDataAnalysis.xls";
                element.style.display = 'none';
                document.body.appendChild(element);
                element.click();
            }
        };
        xhttp.open("POST", resolveClientUrl(url), true);
        xhttp.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
        xhttp.responseType = 'blob';
        xhttp.send(params);
    },

    ExportExcelForWonTickets: function (transList, title) {
        var params = 'transList=' + transList + "&title=" + title;
        var xhttp = new XMLHttpRequest();
        xhttp.onreadystatechange = function () {
            var element;
            if (xhttp.readyState === 4 && xhttp.status === 200) {
                element = document.createElement('a');
                element.href = window.URL.createObjectURL(xhttp.response);
                element.download = "ParlayWonTickets.xls";
                element.style.display = 'none';
                document.body.appendChild(element);
                element.click();
            }
            loading.hide();
        };
        xhttp.open("POST", resolveClientUrl('LeagueBetTypeAnalysis/ExportExcelForWonTickets'), true);
        xhttp.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
        xhttp.responseType = 'blob';
        loading.show();
        xhttp.send(params);
    },

    ResetCachedObject: function () {
        LeagueBetTypeAnalysis.cachedObject.fromDate = null;
        LeagueBetTypeAnalysis.cachedObject.toDate = null;
        LeagueBetTypeAnalysis.cachedObject.parlayType = null;
        LeagueBetTypeAnalysis.cachedObject.leagueGroups = null;
        LeagueBetTypeAnalysis.cachedObject.leagues = null;
        LeagueBetTypeAnalysis.cachedObject.site = null;
        LeagueBetTypeAnalysis.cachedObject.ratio = null;
        LeagueBetTypeAnalysis.cachedObject.reportBy = 0;
        LeagueBetTypeAnalysis.cachedObject.sportType = 1;
        LeagueBetTypeAnalysis.cachedObject.betType = 0;
        LeagueBetTypeAnalysis.cachedObject.houseWL = 0;
        LeagueBetTypeAnalysis.cachedObject.sortById = null;
        LeagueBetTypeAnalysis.cachedObject.isSortAsc = null;
        LeagueBetTypeAnalysis.cachedObject.page = 1;
        LeagueBetTypeAnalysis.cachedObject.pageSize = 100;
    },

    IsNumber: function (event) {
        var code = event.keyCode;
        if (code >= 48 && code <= 57) {
            return true;
        }

        return false;
    },

    ReloadLeagues: function (leagueGroups) {
        var leagueGroupsList = leagueGroups.split(',');
        var data = LeagueBetTypeAnalysis.LeagueList.filter(function (obj) {
            return leagueGroupsList.indexOf(obj.GroupId.toString()) > -1
        });
        var html = '';
        $.each(data, function (key, obj) {
            html += '<option value="' + obj.LeagueId + '" aria-selected="true">' + obj.LeagueName + '</option>';
        });
        $('#ddlLeague').html(html);
        $('#ddlLeague').multiselect('refresh');
        $("#ddlLeague").multiselect("checkAll");
    },

    ReloadLeagueGroups: function (data) {
        var html = '';
        $.each(data, function (key, obj) {
            html += '<option value="' + obj.Id + '" aria-selected="true">' + obj.Name + '</option>';
        });
        $('#ddlGroup').html(html);
        $('#ddlGroup').multiselect('refresh');
        $("#ddlGroup").multiselect("checkAll");
    },

    LoadBettypeControl: function (data) {
        var html = '';
        $.each(data, function (key, obj) {
            if (obj.GroupId == 1) {
                html += '<optgroup label="Major Bet Types">';
            }
            else {
                html += '<optgroup label="Minor Bet Types">';
            }
            $.each(obj.Items, function (inx, bettype) {
                html += '<option value="' + bettype.Bettype + '">' + bettype.TypeNamee + '</option>';
            })
            html += '</optgroup>';
        });

        $('#ddlBetType').html(html);
        $('#ddlBetType').multiselect('refresh');
    },

    IsDisabled: function (reportBy, leagueGroups, leagues) {
        return leagueGroups == '-1' || leagueGroups == '' ||
            (leagueGroups.split(',').length == LeagueBetTypeAnalysis.totalLeagueGroup
                && reportBy == '2');
    },

    HandleLeagueControl: function (type) {
        var reportBy = $('input[name=rdlReportBy]:checked').val();
        var leagueGroups = $("#ddlGroup").multiselect("getChecked").map(function () {
            return this.value;
        }).get().join(',');
        if (reportBy == '2') {
            if (type == 2 && leagueGroups.split(',').length == LeagueBetTypeAnalysis.totalLeagueGroup) {
                $("#ddlLeague").multiselect("disable");
            }
            if (type == 1 && leagueGroups.split(',').length == LeagueBetTypeAnalysis.totalLeagueGroup) {
                $("#ddlLeague").multiselect("disable");
                LeagueBetTypeAnalysis.ReloadLeagues(leagueGroups);
            }
        }
    },

    HandleElementForLeagueBetTypeTab: function () {
        $(LeagueBetTypeAnalysis.DOM.houseWinLossDiv).hide();
        $(LeagueBetTypeAnalysis.DOM.reportByDiv).show();
        $(LeagueBetTypeAnalysis.DOM.ratioDiv).show();
        $(LeagueBetTypeAnalysis.DOM.betTypeDiv).hide();
        $(LeagueBetTypeAnalysis.DOM.leagueDiv).show();
    },

    HandleElementForLeagueTab: function (leagueId) {
        $(LeagueBetTypeAnalysis.DOM.houseWinLossDiv).show();
        $(LeagueBetTypeAnalysis.DOM.reportByDiv).hide();
        $(LeagueBetTypeAnalysis.DOM.ratioDiv).hide();
        if (leagueId > 0) {
            $("#ddlLeague").val([leagueId]).multiselect("refresh");
        }
        $(LeagueBetTypeAnalysis.DOM.betTypeDiv).hide();
        $(LeagueBetTypeAnalysis.DOM.leagueDiv).show();
    },

    HandleElementForBetTypeTab: function (bettype) {
        bettype !== '' ? $("#ddlBetType").val([bettype]).multiselect("refresh") : $("#ddlBetType").multiselect("checkAll");
        $(LeagueBetTypeAnalysis.DOM.houseWinLossDiv).show();
        $(LeagueBetTypeAnalysis.DOM.reportByDiv).hide();
        $(LeagueBetTypeAnalysis.DOM.ratioDiv).hide();
        $(LeagueBetTypeAnalysis.DOM.betTypeDiv).show();
        $(LeagueBetTypeAnalysis.DOM.leagueDiv).hide();
    }
}

$(document).ready(function () {
    LeagueBetTypeAnalysis.Init();
});

function MovePage(pageNumber) {
    LeagueBetTypeAnalysis.Data(LeagueBetTypeAnalysis.cachedObject.parlayType,
        LeagueBetTypeAnalysis.cachedObject.fromDate,
        LeagueBetTypeAnalysis.cachedObject.toDate,
        LeagueBetTypeAnalysis.cachedObject.leagueGroups,
        LeagueBetTypeAnalysis.cachedObject.leagues,
        LeagueBetTypeAnalysis.cachedObject.site,
        LeagueBetTypeAnalysis.cachedObject.ratio,
        LeagueBetTypeAnalysis.cachedObject.reportBy,
        LeagueBetTypeAnalysis.cachedObject.sportType,
        LeagueBetTypeAnalysis.cachedObject.betType,
        LeagueBetTypeAnalysis.cachedObject.houseWL,
        LeagueBetTypeAnalysis.cachedObject.sortById,
        LeagueBetTypeAnalysis.cachedObject.isSortAsc,
        pageNumber,
        LeagueBetTypeAnalysis.cachedObject.pageSize);
}

