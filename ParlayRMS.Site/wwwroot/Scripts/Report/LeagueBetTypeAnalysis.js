LeagueBetTypeAnalysis = {
    DOM: {
        ddlLeague: "#ddlLeague",
        ddlLeagueOfLeague: "#ddlLeague-League",
        ddlLeagueOfBetType: "#ddlLeague-BetType",
        ddlGroup: "#ddlGroup",
        ddlGroupOfLeague: "#ddlGroup-League",
        ddlGroupOfBetType: "#ddlGroup-BetType",
        ddlParlayType: "#ddlParlayType",
        ddlParlayTypeOfLeague: "#ddlParlayType-League",
        ddlParlayTypeOfBetType: "#ddlParlayType-BetType",
        ddlSite: "#ddlSite",
        ddlSiteOfLeague: "#ddlSite-League",
        ddlSiteOfBetType: "#ddlSite-BetType",
        ddlSport: "#ddlSport",
        ddlSportOfLeague: "#ddlSport-League",
        ddlSportOfBetType: "#ddlSport-BetType",
        fromDate: "#fromDate",
        fromDateOfLeague: "#fromDate-League",
        fromDateOfBetType: "#fromDate-BetType",
        toDate: "#toDate",
        toDateOfLeague: "#toDate-League",
        toDateOfBetType: "#toDate-BetType",
        btnSearch: ".btnSearch",
        txtHouseWLOfLeague: "#txtHouseWL-League",
        txtHouseWLOfBetType: "#txtHouseWL-BetType",
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
            leagueTitle: null,
            betTypeTitle: null,
            houseWL: null,
            isDetail: null,
            sortById: null,
            isSortAsc: null,
            page: 1,
            pageSize: 100
        };
        var nowDate = moment(dateRanges.NowDate).format('MMMM DD, YYYY');
        var minDate = moment(dateRanges.LimitDataFromDate).format('MMMM DD, YYYY');

        $(LeagueBetTypeAnalysis.DOM.txtHouseWLOfLeague).autoNumeric({
            aSep: ',',
            aDec: '.',
            vMin: '-99999999999',
            vMax: '99999999999',
            aPad: false,
            aForm: false
        });

        $(LeagueBetTypeAnalysis.DOM.txtHouseWLOfBetType).autoNumeric({
            aSep: ',',
            aDec: '.',
            vMin: '-99999999999',
            vMax: '99999999999',
            aPad: false,
            aForm: false
        });

        $("#tabs").tabs({
            activate: function (event, ui) {
            },
            beforeActivate: function (event, ui) {
                Validators.hide();
            }
        });
        $("#tabs").removeClass("ui-widget");

        $(LeagueBetTypeAnalysis.DOM.ddlParlayType).multiselect({
            buttonWidth: 165,
            menuHeight: 155,
            create: function () {
                $(LeagueBetTypeAnalysis.DOM.ddlParlayType).multiselect("checkAll");
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
        });

        $(LeagueBetTypeAnalysis.DOM.ddlParlayTypeOfLeague).multiselect({
            buttonWidth: 165,
            menuHeight: 155,
            create: function () {
                $(LeagueBetTypeAnalysis.DOM.ddlParlayTypeOfLeague).multiselect("checkAll");
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
        });

        $(LeagueBetTypeAnalysis.DOM.ddlParlayTypeOfBetType).multiselect({
            buttonWidth: 165,
            menuHeight: 155,
            create: function () {
                $(LeagueBetTypeAnalysis.DOM.ddlParlayTypeOfBetType).multiselect("checkAll");
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
        });

        $(LeagueBetTypeAnalysis.DOM.ddlGroup).multiselect({
            buttonWidth: 176,
            menuHeight: 250,
            selectedList: 1,
            create: function () {
                $(LeagueBetTypeAnalysis.DOM.ddlGroup).multiselect();
            },
            checkAll: function () { LeagueBetTypeAnalysis.HandleLeagueControl(1, $(LeagueBetTypeAnalysis.DOM.ddlLeague), $(LeagueBetTypeAnalysis.DOM.ddlGroup)) },
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

        $(LeagueBetTypeAnalysis.DOM.ddlGroupOfLeague).multiselect({
            buttonWidth: 176,
            menuHeight: 250,
            selectedList: 1,
            create: function () {
                $(LeagueBetTypeAnalysis.DOM.ddlGroupOfLeague).multiselect();
            },
            checkAll: function () { LeagueBetTypeAnalysis.HandleLeagueControl(1, $(LeagueBetTypeAnalysis.DOM.ddlLeagueOfLeague), $(LeagueBetTypeAnalysis.DOM.ddlGroupOfLeague)) },
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

        $(LeagueBetTypeAnalysis.DOM.ddlGroupOfBetType).multiselect({
            buttonWidth: 176,
            menuHeight: 250,
            selectedList: 1,
            create: function () {
                $(LeagueBetTypeAnalysis.DOM.ddlGroupOfBetType).multiselect();
            },
            checkAll: function () { LeagueBetTypeAnalysis.HandleLeagueControl(1, $(LeagueBetTypeAnalysis.DOM.ddlLeagueOfBetType), $(LeagueBetTypeAnalysis.DOM.ddlGroupOfBetType)) },
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
            header: false,
            multiple: false,
            selectedList: 1,
        })

        $(LeagueBetTypeAnalysis.DOM.ddlLeague).multiselect({
            beforeopen: function () { $(this).multiselect('widget').width(200); },
            buttonWidth: 200,
            menuHeight: 200,
            create: function () {
                $(LeagueBetTypeAnalysis.DOM.ddlLeague).multiselect("checkAll");
            },
            checkAll: function () { LeagueBetTypeAnalysis.HandleLeagueControl(2, $(LeagueBetTypeAnalysis.DOM.ddlLeague), $(LeagueBetTypeAnalysis.DOM.ddlGroup)) },
            selectedText: function (numChecked, numTotal, checkedItems) {
                if (numChecked == 0) {
                    return "Select options";
                }
                else if (numChecked == 1) {
                    var selectedText = checkedItems[0].labels[0].innerText;
                    var characters = parseInt($(LeagueBetTypeAnalysis.DOM.ddlSport).val()) === 1 ? 20 : 24;
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

        $(LeagueBetTypeAnalysis.DOM.ddlLeagueOfLeague).multiselect({
            beforeopen: function () { $(this).multiselect('widget').width(200); },
            buttonWidth: 200,
            menuHeight: 200,
            create: function () {
                $(LeagueBetTypeAnalysis.DOM.ddlLeagueOfLeague).multiselect("checkAll");
            },
            checkAll: function () { LeagueBetTypeAnalysis.HandleLeagueControl(2, $(LeagueBetTypeAnalysis.DOM.ddlLeagueOfLeague), $(LeagueBetTypeAnalysis.DOM.ddlGroupOfLeague)) },
            selectedText: function (numChecked, numTotal, checkedItems) {
                if (numChecked == 0) {
                    return "Select options";
                }
                else if (numChecked == 1) {
                    var selectedText = checkedItems[0].labels[0].innerText;
                    var characters = parseInt($(LeagueBetTypeAnalysis.DOM.ddlSportOfLeague).val()) === 1 ? 20 : 24;
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

        $(LeagueBetTypeAnalysis.DOM.ddlSite).multiselect({
            buttonWidth: 80,
            menuHeight: 100,
            header: false,
            multiple: false,
            selectedList: 1
        });

        $(LeagueBetTypeAnalysis.DOM.ddlSiteOfLeague).multiselect({
            buttonWidth: 80,
            menuHeight: 100,
            header: false,
            multiple: false,
            selectedList: 1
        });

        $(LeagueBetTypeAnalysis.DOM.ddlSiteOfBetType).multiselect({
            buttonWidth: 80,
            menuHeight: 100,
            header: false,
            multiple: false,
            selectedList: 1
        });

        $(LeagueBetTypeAnalysis.DOM.ddlSport).multiselect({
            buttonWidth: 90,
            menuHeight: 80,
            header: false,
            multiple: false,
            selectedList: 1
        });

        $(LeagueBetTypeAnalysis.DOM.ddlSportOfLeague).multiselect({
            buttonWidth: 90,
            menuHeight: 80,
            header: false,
            multiple: false,
            selectedList: 1
        });

        $(LeagueBetTypeAnalysis.DOM.ddlSportOfBetType).multiselect({
            buttonWidth: 90,
            menuHeight: 80,
            header: false,
            multiple: false,
            selectedList: 1
        });

        $(LeagueBetTypeAnalysis.DOM.fromDate).datepicker({
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
                $(LeagueBetTypeAnalysis.DOM.toDate).datepicker("option", "minDate", selectedDate);
            }
        });

        $(LeagueBetTypeAnalysis.DOM.toDate).datepicker({
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
                $(LeagueBetTypeAnalysis.DOM.fromDate).datepicker("option", "maxDate", selectedDate);
            }
        });

        $(LeagueBetTypeAnalysis.DOM.fromDateOfLeague).datepicker({
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
                $(LeagueBetTypeAnalysis.DOM.toDateOfLeague).datepicker("option", "minDate", selectedDate);
            }
        });

        $(LeagueBetTypeAnalysis.DOM.toDateOfLeague).datepicker({
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
                $(LeagueBetTypeAnalysis.DOM.fromDateOfLeague).datepicker("option", "maxDate", selectedDate);
            }
        });

        $(LeagueBetTypeAnalysis.DOM.fromDateOfBetType).datepicker({
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
                $(LeagueBetTypeAnalysis.DOM.toDateOfBetType).datepicker("option", "minDate", selectedDate);
            }
        });

        $(LeagueBetTypeAnalysis.DOM.toDateOfBetType).datepicker({
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
                $(LeagueBetTypeAnalysis.DOM.fromDateOfBetType).datepicker("option", "maxDate", selectedDate);
            }
        });

        var nowDate = moment(dateRanges.NowDate);
        $(LeagueBetTypeAnalysis.DOM.fromDate + ',' + LeagueBetTypeAnalysis.DOM.fromDateOfLeague + ',' + LeagueBetTypeAnalysis.DOM.fromDateOfBetType).val(nowDate.format('MMMM DD, YYYY'));
        $(LeagueBetTypeAnalysis.DOM.toDate + ',' + LeagueBetTypeAnalysis.DOM.toDateOfLeague + ',' + LeagueBetTypeAnalysis.DOM.toDateOfBetType).val(nowDate.format('MMMM DD, YYYY'));
        $(LeagueBetTypeAnalysis.DOM.btnSearch).on('click', function () {
            Validators.hide();
            $("#divResult").html('');
            LeagueBetTypeAnalysis.ResetCachedObject();

            var fromDate = null;
            var toDate = null;
            var parlayType = '';
            var leagueGroups = '';
            var leagues = '';
            var site = 2;
            var betType = '';
            var sportType = 1;
            var ratio = 0;
            var houseWL = 0;
            var isDetail = false;
            var reportBy = 1;
            var betTypeTitle = null;
            var leagueTitle = null;

            if ($("#tabs").tabs('option', 'active') == 0) {
                fromDate = $(LeagueBetTypeAnalysis.DOM.fromDate).val();
                toDate = $(LeagueBetTypeAnalysis.DOM.toDate).val();

                if (validator.validateDate(LeagueBetTypeAnalysis.DOM.fromDate)) {
                    return;
                }

                if (validator.validateDate(LeagueBetTypeAnalysis.DOM.toDate)) {
                    return;
                }

                parlayType = $(LeagueBetTypeAnalysis.DOM.ddlParlayType).multiselect("getChecked").map(function () {
                    return this.value;
                }).get().join(',');
                if (parlayType == '') {
                    Validators.show($(LeagueBetTypeAnalysis.DOM.ddlParlayType).multiselect("getButton").get(0), 'Please select your options');
                    return;
                }

                leagueGroups = $(LeagueBetTypeAnalysis.DOM.ddlGroup).multiselect("getChecked").map(function () {
                    return this.value;
                }).get().join(',');
                if (leagueGroups == '') {
                    Validators.show($(LeagueBetTypeAnalysis.DOM.ddlGroup).multiselect("getButton").get(0), 'Please select your options');
                    return;
                }

                leagues = '';
                if (leagueGroups == '-1') {
                    leagues = '-1';
                } else {
                    leagues = $(LeagueBetTypeAnalysis.DOM.ddlLeague).multiselect("getChecked").map(function () {
                        return this.value;
                    }).get().join(',');
                    if ($(LeagueBetTypeAnalysis.DOM.ddlLeague).multiselect("getButton").get(0).innerText == 'All' && leagueGroups.search('-1') > -1) {
                        leagues = '-1,' + leagues;
                    }
                }

                if (leagues == '') {
                    Validators.show($("#ddlLeague").multiselect("getButton").get(0), 'Please select your options');
                    return;
                }

                reportBy = $('input[name=rdlReportBy]:checked').val();

                sportType = parseInt($(LeagueBetTypeAnalysis.DOM.ddlSport).val());

                if (leagueGroups == '-1' && reportBy == '2') {
                    Validators.show($(LeagueBetTypeAnalysis.DOM.ddlGroup).multiselect("getButton").get(0), 'Please select another option to get correct report!');
                }

                if (leagues.split(',').length > 1 && reportBy == '2' && $(LeagueBetTypeAnalysis.DOM.ddlGroup).multiselect("getButton").get(0).innerText != 'All') {
                    Validators.show($(LeagueBetTypeAnalysis.DOM.ddlLeague).multiselect("getButton").get(0), 'Please select single League to get correct report!');
                }

                site = parseInt($(LeagueBetTypeAnalysis.DOM.ddlSite).val());

                ratio = isNaN(parseInt($("#txtRatio").val())) ? 0 : parseFloat($("#txtRatio").val()) / 100;

                if (ratio > 1) {
                    Validators.show($("#txtRatio"), 'Invalid Ratio');
                    return;
                }
            }

            if ($("#tabs").tabs('option', 'active') == 1) {
                fromDate = $(LeagueBetTypeAnalysis.DOM.fromDateOfLeague).val();
                toDate = $(LeagueBetTypeAnalysis.DOM.toDateOfLeague).val();

                if (validator.validateDate(LeagueBetTypeAnalysis.DOM.fromDateOfLeague)) {
                    return;
                }

                if (validator.validateDate(LeagueBetTypeAnalysis.DOM.toDateOfLeague)) {
                    return;
                }

                parlayType = $(LeagueBetTypeAnalysis.DOM.ddlParlayTypeOfLeague).multiselect("getChecked").map(function () {
                    return this.value;
                }).get().join(',');
                if (parlayType == '') {
                    Validators.show($(LeagueBetTypeAnalysis.DOM.ddlParlayTypeOfLeague).multiselect("getButton").get(0), 'Please select your options');
                    return;
                }

                leagueGroups = $(LeagueBetTypeAnalysis.DOM.ddlGroupOfLeague).multiselect("getChecked").map(function () {
                    return this.value;
                }).get().join(',');
                if (leagueGroups == '') {
                    Validators.show($(LeagueBetTypeAnalysis.DOM.ddlGroupOfLeague).multiselect("getButton").get(0), 'Please select your options');
                    return;
                }

                leagues = '';
                if (leagueGroups == '-1') {
                    leagues = '-1';
                } else {
                    leagues = $(LeagueBetTypeAnalysis.DOM.ddlLeagueOfLeague).multiselect("getChecked").map(function () {
                        return this.value;
                    }).get().join(',');
                    if ($(LeagueBetTypeAnalysis.DOM.ddlLeagueOfLeague).multiselect("getButton").get(0).innerText == 'All' && leagueGroups.search('-1') > -1) {
                        leagues = '-1,' + leagues;
                    }
                }

                if (leagues == '') {
                    Validators.show($("#ddlLeague").multiselect("getButton").get(0), 'Please select your options');
                    return;
                }

                if (leagues.split(',').length === 1) {
                    leagueTitle = $(LeagueBetTypeAnalysis.DOM.ddlLeagueOfLeague + ' option:selected').toArray().map(item => item.text).join()
                }

                sportType = parseInt($(LeagueBetTypeAnalysis.DOM.ddlSportOfLeague).val());

                site = parseInt($(LeagueBetTypeAnalysis.DOM.ddlSiteOfLeague).val());

                reportBy = 1;
                houseWL = $.trim($(LeagueBetTypeAnalysis.DOM.txtHouseWLOfLeague).val().replace(/,/g, ''));
                isDetail = true;
            }

            if ($("#tabs").tabs('option', 'active') == 2) {
                fromDate = $(LeagueBetTypeAnalysis.DOM.fromDateOfBetType).val();
                toDate = $(LeagueBetTypeAnalysis.DOM.toDateOfBetType).val();

                if (validator.validateDate(LeagueBetTypeAnalysis.DOM.fromDateOfBetType)) {
                    return;
                }

                if (validator.validateDate(LeagueBetTypeAnalysis.DOM.toDateOfBetType)) {
                    return;
                }

                parlayType = $(LeagueBetTypeAnalysis.DOM.ddlParlayTypeOfBetType).multiselect("getChecked").map(function () {
                    return this.value;
                }).get().join(',');
                if (parlayType == '') {
                    Validators.show($(LeagueBetTypeAnalysis.DOM.ddlParlayTypeOfBetType).multiselect("getButton").get(0), 'Please select your options');
                    return;
                }

                leagueGroups = $(LeagueBetTypeAnalysis.DOM.ddlGroupOfBetType).multiselect("getChecked").map(function () {
                    return this.value;
                }).get().join(',');
                if (leagueGroups == '') {
                    Validators.show($(LeagueBetTypeAnalysis.DOM.ddlGroupOfBetType).multiselect("getButton").get(0), 'Please select your options');
                    return;
                }

                sportType = parseInt($(LeagueBetTypeAnalysis.DOM.ddlSportOfBetType).val());

                if (leagueGroups == '-1') {
                    Validators.show($(LeagueBetTypeAnalysis.DOM.ddlGroupOfBetType).multiselect("getButton").get(0), 'Please select another option to get correct report!');
                }

                site = parseInt($(LeagueBetTypeAnalysis.DOM.ddlSiteOfBetType).val());
                reportBy = 2;
                betType = parseInt($("#ddlBetType").val());
                isDetail = true;
                houseWL = $.trim($(LeagueBetTypeAnalysis.DOM.txtHouseWLOfBetType).val().replace(/,/g, ''));
                leagues = $(LeagueBetTypeAnalysis.DOM.ddlLeague).multiselect("getChecked").map(function () {
                    return this.value;
                }).get().join(',');
                betTypeTitle = $("#ddlBetType").multiselect("getButton").get(0).innerText;
            }

            $('#header').find(".sortable").each(function () {
                $(this).removeClass("headerSortDown").removeClass("headerSortUp");
                this.sortDirection = "";
            });

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
                isDetail,
                leagueTitle,
                betTypeTitle,
                LeagueBetTypeAnalysis.cachedObject.sortById,
                LeagueBetTypeAnalysis.cachedObject.isSortAsc,
                LeagueBetTypeAnalysis.cachedObject.page,
                LeagueBetTypeAnalysis.cachedObject.pageSize);
        })
        $(LeagueBetTypeAnalysis.DOM.ddlGroup).on('change', function () {
            var leagueGroups = $(LeagueBetTypeAnalysis.DOM.ddlGroup).multiselect("getChecked").map(function () {
                return this.value;
            }).get().join(',');
            var leagues = $(LeagueBetTypeAnalysis.DOM.ddlLeague).multiselect("getChecked").map(function () {
                return this.value;
            }).get().join(',');
            if (LeagueBetTypeAnalysis.IsDisabled($('input[name=rdlReportBy]:checked').val(), leagueGroups, leagues)) {
                $(LeagueBetTypeAnalysis.DOM.ddlLeague).multiselect("disable");
                if (leagueGroups == '-1') {
                    var html = leagueGroups == '' ? '' : '<option value="0" aria-selected="true">All</option>';
                    $(LeagueBetTypeAnalysis.DOM.ddlLeague).html(html);
                    $(LeagueBetTypeAnalysis.DOM.ddlLeague).multiselect('refresh');
                }
            } else {
                $(LeagueBetTypeAnalysis.DOM.ddlLeague).multiselect("enable");
                LeagueBetTypeAnalysis.ReloadLeagues(leagueGroups, LeagueBetTypeAnalysis.DOM.ddlLeague);
            }
        });

        $(LeagueBetTypeAnalysis.DOM.ddlGroupOfLeague).on('change', function () {
            var leagueGroups = $(LeagueBetTypeAnalysis.DOM.ddlGroupOfLeague).multiselect("getChecked").map(function () {
                return this.value;
            }).get().join(',');
            var leagues = $(LeagueBetTypeAnalysis.DOM.ddlLeagueOfLeague).multiselect("getChecked").map(function () {
                return this.value;
            }).get().join(',');
            if (LeagueBetTypeAnalysis.IsDisabled(1, leagueGroups, leagues)) {
                $(LeagueBetTypeAnalysis.DOM.ddlLeagueOfLeague).multiselect("disable");
                if (leagueGroups == '-1') {
                    var html = leagueGroups == '' ? '' : '<option value="0" aria-selected="true">All</option>';
                    $(LeagueBetTypeAnalysis.DOM.ddlLeagueOfLeague).html(html);
                    $(LeagueBetTypeAnalysis.DOM.ddlLeagueOfLeague).multiselect('refresh');
                }
            } else {
                $(LeagueBetTypeAnalysis.DOM.ddlLeagueOfLeague).multiselect("enable");
                LeagueBetTypeAnalysis.ReloadLeagues(leagueGroups, LeagueBetTypeAnalysis.DOM.ddlLeagueOfLeague);
            }
        });

        $('input[name=rdlReportBy]').on('change', function () {
            var leagueGroups = $(LeagueBetTypeAnalysis.DOM.ddlGroup).multiselect("getChecked").map(function () {
                return this.value;
            }).get().join(',');
            var leagues = $(LeagueBetTypeAnalysis.DOM.ddlLeague).multiselect("getChecked").map(function () {
                return this.value;
            }).get().join(',');
            if (LeagueBetTypeAnalysis.IsDisabled(this.value, leagueGroups, leagues)) {
                $(LeagueBetTypeAnalysis.DOM.ddlLeague).multiselect("disable");
                if (leagueGroups.split(',').length == LeagueBetTypeAnalysis.totalLeagueGroup) {
                    LeagueBetTypeAnalysis.ReloadLeagues(leagueGroups, LeagueBetTypeAnalysis.DOM.ddlLeague);
                }
            } else {
                $(LeagueBetTypeAnalysis.DOM.ddlLeague).multiselect("enable");
            }
        });

        LeagueBetTypeAnalysis.GroupsAndLeagues(1, LeagueBetTypeAnalysis.DOM.ddlGroup, LeagueBetTypeAnalysis.DOM.ddlLeague, $("#fromDate").val(), $("#toDate").val());
        LeagueBetTypeAnalysis.GroupsAndLeagues(1, LeagueBetTypeAnalysis.DOM.ddlGroupOfLeague, LeagueBetTypeAnalysis.DOM.ddlLeagueOfLeague, $("#fromDate").val(), $("#toDate").val());
        LeagueBetTypeAnalysis.GroupsAndLeagues(1, LeagueBetTypeAnalysis.DOM.ddlGroupOfBetType, LeagueBetTypeAnalysis.DOM.ddlLeagueOfBetType, $("#fromDate").val(), $("#toDate").val());

        LeagueBetTypeAnalysis.BetTypes(1);

        $(LeagueBetTypeAnalysis.DOM.ddlSport).on('change', function () {
            LeagueBetTypeAnalysis.GroupsAndLeagues(parseInt($(this).val()), LeagueBetTypeAnalysis.DOM.ddlGroup, LeagueBetTypeAnalysis.DOM.ddlLeague, $("#fromDate").val(), $("#toDate").val());
        });

        $(LeagueBetTypeAnalysis.DOM.ddlSportOfLeague).on('change', function () {
            LeagueBetTypeAnalysis.GroupsAndLeagues(parseInt($(this).val()), LeagueBetTypeAnalysis.DOM.ddlGroupOfLeague, LeagueBetTypeAnalysis.DOM.ddlLeagueOfLeague, $("#fromDate-League").val(), $("#toDate-League").val());
        });

        $(LeagueBetTypeAnalysis.DOM.ddlSportOfBetType).on('change', function () {
            LeagueBetTypeAnalysis.GroupsAndLeagues(parseInt($(this).val()), LeagueBetTypeAnalysis.DOM.ddlGroupOfBetType, LeagueBetTypeAnalysis.DOM.ddlLeagueOfBetType, $("#fromDate-BetType").val(), $("#toDate-BetType").val());
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
                LeagueBetTypeAnalysis.cachedObject.isDetail,
                LeagueBetTypeAnalysis.cachedObject.leagueTitle,
                LeagueBetTypeAnalysis.cachedObject.betTypeTitle,
                LeagueBetTypeAnalysis.cachedObject.sortById,
                LeagueBetTypeAnalysis.cachedObject.isSortAsc,
                LeagueBetTypeAnalysis.cachedObject.page,
                LeagueBetTypeAnalysis.cachedObject.pageSize);
        });

        $(document).on('click', '.leagueTab', function () {
            $("#tabs").tabs("option", "active", 1);
            $(LeagueBetTypeAnalysis.DOM.txtHouseWLOfLeague).val('-50,000');
            var leagueId = this.getAttribute('data-leagueid');
            var leagueName = this.getAttribute('data-leaguename');
            LeagueBetTypeAnalysis.HandleElementForLeagueTab(leagueId);
            LeagueBetTypeAnalysis.Data(LeagueBetTypeAnalysis.cachedObject.parlayType,
                LeagueBetTypeAnalysis.cachedObject.fromDate,
                LeagueBetTypeAnalysis.cachedObject.toDate,
                LeagueBetTypeAnalysis.cachedObject.leagueGroups,
                leagueId,
                LeagueBetTypeAnalysis.cachedObject.site,
                LeagueBetTypeAnalysis.cachedObject.ratio,
                1,
                LeagueBetTypeAnalysis.cachedObject.sportType,
                '',
                $.trim($('#txtHouseWL-League').val().replace(/,/g, '')),
                true,
                leagueName,
                null,
                null,
                null,
                1,
                100);
        });

        $(document).on('click', '.betTypeTab', function () {
            $("#tabs").tabs("option", "active", 2);
            $(LeagueBetTypeAnalysis.DOM.txtHouseWLOfBetType).val('-50,000');
            var bettype = this.getAttribute('data-bettypeid');
            var bettypeName = this.getAttribute('data-bettypename');
            var leagues = $(LeagueBetTypeAnalysis.DOM.ddlLeague).multiselect("getChecked").map(function () {
                return this.value;
            }).get().join(',');
            LeagueBetTypeAnalysis.HandleElementForBetTypeTab(bettype);
            LeagueBetTypeAnalysis.Data(LeagueBetTypeAnalysis.cachedObject.parlayType,
                LeagueBetTypeAnalysis.cachedObject.fromDate,
                LeagueBetTypeAnalysis.cachedObject.toDate,
                LeagueBetTypeAnalysis.cachedObject.leagueGroups,
                leagues,
                LeagueBetTypeAnalysis.cachedObject.site,
                LeagueBetTypeAnalysis.cachedObject.ratio,
                2,
                LeagueBetTypeAnalysis.cachedObject.sportType,
                bettype,
                $.trim($('#txtHouseWL-BetType').val().replace(/,/g, '')),
                true,
                null,
                bettypeName,
                null,
                null,
                1,
                100);
        });

        $(document).on('click', '.parlayWonTicket', function () {
            var title = $("#tabs").tabs('option', 'active') === 1 ?
                "Group [" + $(this).data('group') + "] - League [" + $(this).data('league') + "] - Event [" + $(this).data('event') + "] - [" + $(this).data('bettype') + "] - [" + $(this).data('betchoice') + "]"
                : "Group [" + $(this).data('group') + "] - League [" + $(this).data('league') + "] - Event [" + $(this).data('event') + "] - [" + $("#ddlBetType").find("option:checked").text() + "]";
            var bettypeId = $("#tabs").tabs('option', 'active') === 1 ? $(this).data('bettypeid') : LeagueBetTypeAnalysis.cachedObject.betType;
            LeagueBetTypeAnalysis.ExportExcelForWonTickets($(this).data('betteam'), $(this).data('match'), bettypeId, title);
        });
    },

    GroupsAndLeagues: function (sportType, groupElement, leagueElement, fromDate, toDate) {
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
                LeagueBetTypeAnalysis.ReloadLeagueGroups(json.Groups, groupElement);
                LeagueBetTypeAnalysis.totalLeagueGroup = json.Groups.length;
                LeagueBetTypeAnalysis.LeagueList = json.Leagues;
                var leagueGroups = $(groupElement).multiselect("getChecked").map(function () {
                    return this.value;
                }).get().join(',');
                LeagueBetTypeAnalysis.ReloadLeagues(leagueGroups, leagueElement);
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

    Data: function (parlayType, fromDate, toDate, leagueGroups, leagues, site, ratio, reportBy, sportType, betType, houseWL, isDetail, leagueTitle, betTypeTitle, sortById, isSortAsc, page, pageSize) {
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
            isDetail: isDetail,
            leagueTitle: leagueTitle,
            betTypeTitle: betTypeTitle,
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
                        LeagueBetTypeAnalysis.cachedObject.isDetail,
                        LeagueBetTypeAnalysis.cachedObject.leagueTitle,
                        LeagueBetTypeAnalysis.cachedObject.betTypeTitle,
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
    ExportExcel: function (parlayType, fromDate, toDate, leagues, leagueGroups, site, ratio, reportBy, sportType, betType, houseWL, isDetail, leagueTitle, betTypeTitle) {

        var params = 'fromDate=' + fromDate + '&toDate=' + toDate + '&parlayType=' + parlayType + '&leagueGroups=' + leagueGroups
            + '&leagues=' + leagues + '&site=' + site + '&ratio=' + ratio + '&reportBy=' + reportBy + '&sportType=' + sportType
            + '&betType=' + betType + '&houseWL=' + houseWL + '&isDetail=' + isDetail + '&leagueTitle=' + leagueTitle + '&betTypeTitle=' + betTypeTitle;

        var url = '';

        switch (reportBy) {
            case '1':
                url = isDetail === 'True' ? 'LeagueBetTypeAnalysis/ExportExcelForLeagueDetails' : 'LeagueBetTypeAnalysis/ExportExcelForLeague';
                break;
            case '2':
                url = isDetail === 'True' ? 'LeagueBetTypeAnalysis/ExportExcelForBetTypeDetails' : 'LeagueBetTypeAnalysis/ExportExcelForBetType';
                break;
            default:
                url = 'LeagueBetTypeAnalysis/ExportExcelForLeague';
                break;
        }

        var xhttp = new XMLHttpRequest();
        xhttp.onreadystatechange = function () {
            var element;
            if (xhttp.readyState === 4 && xhttp.status === 200) {
                const fileName = xhttp.getResponseHeader('content-disposition').split('filename=')[1].split(';')[0];
                element = document.createElement('a');
                element.href = window.URL.createObjectURL(xhttp.response);
                element.download = fileName;
                element.style.display = 'none';
                document.body.appendChild(element);
                element.click();
            }
            loading.hide();
        };
        xhttp.open("POST", resolveClientUrl(url), true);
        xhttp.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
        xhttp.responseType = 'blob';
        loading.show();
        xhttp.send(params);
    },

    ExportExcelForWonTickets: function (betTeam, match, bettypeId, title) {
        var params = 'fromDate=' + LeagueBetTypeAnalysis.cachedObject.fromDate + '&toDate=' + LeagueBetTypeAnalysis.cachedObject.toDate
            + '&parlayType=' + LeagueBetTypeAnalysis.cachedObject.parlayType + '&leagueGroups=' + LeagueBetTypeAnalysis.cachedObject.leagueGroups
            + '&leagues=' + LeagueBetTypeAnalysis.cachedObject.leagues + '&site=' + LeagueBetTypeAnalysis.cachedObject.site
            + '&reportBy=3' + '&sportType=' + LeagueBetTypeAnalysis.cachedObject.sportType
            + '&betType=' + bettypeId + '&houseWL=' + LeagueBetTypeAnalysis.cachedObject.houseWL + '&betTeam=' + betTeam + '&match=' + match + "&title=" + title;
        var xhttp = new XMLHttpRequest();
        xhttp.onreadystatechange = function () {
            var element;
            if (xhttp.readyState === 4 && xhttp.status === 200) {
                const fileName = xhttp.getResponseHeader('content-disposition').split('filename=')[1].split(';')[0];
                element = document.createElement('a');
                element.href = window.URL.createObjectURL(xhttp.response);
                element.download = fileName;
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

    ReloadLeagues: function (leagueGroups, leagueElement) {
        var leagueGroupsList = leagueGroups.split(',');
        var data = LeagueBetTypeAnalysis.LeagueList.filter(function (obj) {
            return leagueGroupsList.indexOf(obj.GroupId.toString()) > -1
        });
        var html = '';
        $.each(data, function (key, obj) {
            html += '<option value="' + obj.LeagueId + '" aria-selected="true">' + obj.LeagueName + '</option>';
        });
        $(leagueElement).html(html);
        $(leagueElement).multiselect('refresh');
        $(leagueElement).multiselect("checkAll");
    },

    ReloadLeagueGroups: function (data, groupElement) {
        var html = '';
        $.each(data, function (key, obj) {
            html += '<option value="' + obj.Id + '" aria-selected="true">' + obj.Name + '</option>';
        });
        $(groupElement).html(html);
        $(groupElement).multiselect('refresh');
        $(groupElement).multiselect("checkAll");
    },

    LoadBettypeControl: function (data) {
        var html = '';
        $.each(data, function (key, obj) {
            html += '<option value="' + obj.Bettype + '">' + obj.TypeNamee + '</option>';
        });
        $('#ddlBetType').html(html);
        $('#ddlBetType').multiselect('refresh');
    },

    IsDisabled: function (reportBy, leagueGroups, leagues) {
        return leagueGroups == '-1' || leagueGroups == '' ||
            (leagueGroups.split(',').length == LeagueBetTypeAnalysis.totalLeagueGroup
            && reportBy == '2' && $("#tabs").tabs('option', 'active') === 0);
    },

    HandleLeagueControl: function (type, leagueElement, groupElement) {
        var reportBy = $('input[name=rdlReportBy]:checked').val();
        var leagueGroups = groupElement.multiselect("getChecked").map(function () {
            return this.value;
        }).get().join(',');
        if (reportBy == '2') {
            if (type == 2 && leagueGroups.split(',').length == LeagueBetTypeAnalysis.totalLeagueGroup) {
                leagueElement.multiselect("disable");
            }
            if (type == 1 && leagueGroups.split(',').length == LeagueBetTypeAnalysis.totalLeagueGroup) {
                leagueElement.multiselect("disable");
                LeagueBetTypeAnalysis.ReloadLeagues(leagueGroups, leagueElement);
            }
        }
    },

    HandleElementForLeagueTab: function (leagueId) {
        $(LeagueBetTypeAnalysis.DOM.fromDateOfLeague).datepicker("setDate", LeagueBetTypeAnalysis.cachedObject.fromDate);
        $(LeagueBetTypeAnalysis.DOM.toDateOfLeague).datepicker("setDate", LeagueBetTypeAnalysis.cachedObject.toDate);
        $(LeagueBetTypeAnalysis.DOM.ddlSportOfLeague).val(LeagueBetTypeAnalysis.cachedObject.sportType).multiselect("refresh");
        $(LeagueBetTypeAnalysis.DOM.ddlSiteOfLeague).val(LeagueBetTypeAnalysis.cachedObject.site).multiselect("refresh");
        $(LeagueBetTypeAnalysis.DOM.ddlParlayTypeOfLeague).val(LeagueBetTypeAnalysis.cachedObject.parlayType.split(',')).multiselect("refresh");
        $(LeagueBetTypeAnalysis.DOM.ddlGroupOfLeague).val(LeagueBetTypeAnalysis.cachedObject.leagueGroups.split(',')).multiselect("refresh");
        if (leagueId > 0) {
            $(LeagueBetTypeAnalysis.DOM.ddlLeagueOfLeague).val([leagueId]).multiselect("refresh");
        }
    },

    HandleElementForBetTypeTab: function (bettype) {
        $("#ddlBetType").val(bettype);
        $(LeagueBetTypeAnalysis.DOM.fromDateOfBetType).datepicker("setDate", LeagueBetTypeAnalysis.cachedObject.fromDate);
        $(LeagueBetTypeAnalysis.DOM.toDateOfBetType).datepicker("setDate", LeagueBetTypeAnalysis.cachedObject.toDate);
        $(LeagueBetTypeAnalysis.DOM.ddlSportOfBetType).val(LeagueBetTypeAnalysis.cachedObject.sportType).multiselect("refresh");
        $(LeagueBetTypeAnalysis.DOM.ddlSiteOfBetType).val(LeagueBetTypeAnalysis.cachedObject.site).multiselect("refresh");
        $(LeagueBetTypeAnalysis.DOM.ddlParlayTypeOfBetType).val(LeagueBetTypeAnalysis.cachedObject.parlayType.split(',')).multiselect("refresh");
        $(LeagueBetTypeAnalysis.DOM.ddlGroupOfBetType).val(LeagueBetTypeAnalysis.cachedObject.leagueGroups.split(',')).multiselect("refresh");
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
        LeagueBetTypeAnalysis.cachedObject.isDetail,
        LeagueBetTypeAnalysis.cachedObject.leagueTitle,
        LeagueBetTypeAnalysis.cachedObject.betTypeTitle,
        LeagueBetTypeAnalysis.cachedObject.sortById,
        LeagueBetTypeAnalysis.cachedObject.isSortAsc,
        pageNumber,
        LeagueBetTypeAnalysis.cachedObject.pageSize);
}

