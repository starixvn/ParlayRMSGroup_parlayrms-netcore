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
        divResult: "#divResult",
        divResultOfLeague: "#divResult-League",
        divResultOfBetType: "#divResult-BetType",
        ddlCombo: "#ddlCombo",
        ddlComboOfLeague: "#ddlCombo-League",
        ddlComboOfBetType: "#ddlCombo-BetType",
        txtUsername: "#username",
        txtUsernameOfLeague: "#username-League",
        txtUsernameOfBetType: "#username-BetType"
    },
    cachedObject: {},
    cachedObjectOfLeague: {},
    cachedObjectOfBetType: {},
    LeagueList: [],
    totalLeagueGroup: 0,
    Init: function () {
        LeagueBetTypeAnalysis.cachedObject = {
            userName: null,
            combo: null,
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
            header: true,
            multiple: false,
            selectedList: 1,
        }).multiselectfilter({
            autoReset: true
        });

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

        $(LeagueBetTypeAnalysis.DOM.ddlLeagueOfBetType).multiselect({
            beforeopen: function () { $(this).multiselect('widget').width(200); },
            buttonWidth: 200,
            menuHeight: 200,
            create: function () {
                $(LeagueBetTypeAnalysis.DOM.ddlLeagueOfBetType).multiselect("checkAll");
            },
            checkAll: function () { LeagueBetTypeAnalysis.HandleLeagueControl(2, $(LeagueBetTypeAnalysis.DOM.ddlLeagueOfBetType), $(LeagueBetTypeAnalysis.DOM.ddlGroupOfBetType)) },
            selectedText: function (numChecked, numTotal, checkedItems) {
                if (numChecked == 0) {
                    return "Select options";
                }
                else if (numChecked == 1) {
                    var selectedText = checkedItems[0].labels[0].innerText;
                    var characters = parseInt($(LeagueBetTypeAnalysis.DOM.ddlSportOfBetType).val()) === 1 ? 20 : 24;
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

        $(LeagueBetTypeAnalysis.DOM.ddlCombo).multiselect({
            buttonWidth: 135,
            menuHeight: 200,
            create: function () {
                $(LeagueBetTypeAnalysis.DOM.ddlCombo).multiselect("checkAll");
            },
            selectedText: function (numChecked, numTotal, checkedItems) {
                if (numChecked == 0) {
                    return "Select options";
                }
                else if (numChecked == 1) {                    
                    return $(checkedItems).val();
                }
                else if (numChecked == numTotal) {
                    return "All";
                }
                else {
                    return numChecked + ' options';
                }
            }
        });

        $(LeagueBetTypeAnalysis.DOM.ddlComboOfLeague).multiselect({
            buttonWidth: 135,
            menuHeight: 200,
            create: function () {
                $(LeagueBetTypeAnalysis.DOM.ddlComboOfLeague).multiselect("checkAll");
            },
            selectedText: function (numChecked, numTotal, checkedItems) {
                if (numChecked == 0) {
                    return "Select options";
                }
                else if (numChecked == 1) {
                    return $(checkedItems).val();
                }
                else if (numChecked == numTotal) {
                    return "All";
                }
                else {
                    return numChecked + ' options';
                }
            }
        });

        $(LeagueBetTypeAnalysis.DOM.ddlComboOfBetType).multiselect({
            buttonWidth: 135,
            menuHeight: 200,
            create: function () {
                $(LeagueBetTypeAnalysis.DOM.ddlComboOfBetType).multiselect("checkAll");
            },
            selectedText: function (numChecked, numTotal, checkedItems) {
                if (numChecked == 0) {
                    return "Select options";
                }
                else if (numChecked == 1) {
                    return $(checkedItems).val();
                }
                else if (numChecked == numTotal) {
                    return "All";
                }
                else {
                    return numChecked + ' options';
                }
            }
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
                LeagueBetTypeAnalysis.GroupsAndLeagues(parseInt(parseInt($(LeagueBetTypeAnalysis.DOM.ddlSport).val())),
                    [LeagueBetTypeAnalysis.DOM.ddlGroup],
                    [LeagueBetTypeAnalysis.DOM.ddlLeague],
                    $(this).val(),
                    $(LeagueBetTypeAnalysis.DOM.toDate).val()
                );
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
                LeagueBetTypeAnalysis.GroupsAndLeagues(parseInt(parseInt($(LeagueBetTypeAnalysis.DOM.ddlSport).val())),
                    [LeagueBetTypeAnalysis.DOM.ddlGroup],
                    [LeagueBetTypeAnalysis.DOM.ddlLeague],
                    $(LeagueBetTypeAnalysis.DOM.fromDate).val(),
                    $(this).val()
                );
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
                LeagueBetTypeAnalysis.GroupsAndLeagues(parseInt(parseInt($(LeagueBetTypeAnalysis.DOM.ddlSport).val())),
                    [LeagueBetTypeAnalysis.DOM.ddlGroupOfLeague],
                    [LeagueBetTypeAnalysis.DOM.ddlLeagueOfLeague],
                    $(this).val(),
                    $(LeagueBetTypeAnalysis.DOM.toDateOfLeague).val()
                );
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
                LeagueBetTypeAnalysis.GroupsAndLeagues(parseInt(parseInt($(LeagueBetTypeAnalysis.DOM.ddlSport).val())),
                    [LeagueBetTypeAnalysis.DOM.ddlGroupOfLeague],
                    [LeagueBetTypeAnalysis.DOM.ddlLeagueOfLeague],
                    $(LeagueBetTypeAnalysis.DOM.fromDateOfLeague).val(),
                    $(this).val()
                );
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
                LeagueBetTypeAnalysis.GroupsAndLeagues(parseInt(parseInt($(LeagueBetTypeAnalysis.DOM.ddlSport).val())),
                    [LeagueBetTypeAnalysis.DOM.ddlGroupOfBetType],
                    [LeagueBetTypeAnalysis.DOM.ddlLeagueOfBetType],
                    $(this).val(),
                    $(LeagueBetTypeAnalysis.DOM.toDateOfBetType).val()
                );
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
                LeagueBetTypeAnalysis.GroupsAndLeagues(parseInt(parseInt($(LeagueBetTypeAnalysis.DOM.ddlSport).val())),
                    [LeagueBetTypeAnalysis.DOM.ddlGroupOfBetType],
                    [LeagueBetTypeAnalysis.DOM.ddlLeagueOfBetType],
                    $(LeagueBetTypeAnalysis.DOM.fromDateOfBetType).val(),
                    $(this).val()
                );
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
            var fromDate = null;
            var toDate = null;
            var userName = '';
            var combo = '';
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
                LeagueBetTypeAnalysis.ResetCachedObject();
                $(LeagueBetTypeAnalysis.DOM.divResult).html('');
                fromDate = $(LeagueBetTypeAnalysis.DOM.fromDate).val();
                toDate = $(LeagueBetTypeAnalysis.DOM.toDate).val();

                if (validator.validateDate(LeagueBetTypeAnalysis.DOM.fromDate)) {
                    return;
                }

                if (validator.validateDate(LeagueBetTypeAnalysis.DOM.toDate)) {
                    return;
                }

                combo = $(LeagueBetTypeAnalysis.DOM.ddlCombo).multiselect("getChecked").map(function () {
                    return this.value;
                }).get().join(',');
                if (combo == '') {
                    Validators.show($(LeagueBetTypeAnalysis.DOM.ddlCombo).multiselect("getButton").get(0), 'Please select your options');
                    return;
                }

                userName = $(LeagueBetTypeAnalysis.DOM.txtUsername).val();

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

                leagues = $(LeagueBetTypeAnalysis.DOM.ddlLeague).multiselect("getChecked").map(function () {
                    return this.value;
                }).get().join(',');

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
                LeagueBetTypeAnalysis.ResetCachedObjectOfLeague();
                $(LeagueBetTypeAnalysis.DOM.divResultOfLeague).html('');
                fromDate = $(LeagueBetTypeAnalysis.DOM.fromDateOfLeague).val();
                toDate = $(LeagueBetTypeAnalysis.DOM.toDateOfLeague).val();

                if (validator.validateDate(LeagueBetTypeAnalysis.DOM.fromDateOfLeague)) {
                    return;
                }

                if (validator.validateDate(LeagueBetTypeAnalysis.DOM.toDateOfLeague)) {
                    return;
                }

                userName = $(LeagueBetTypeAnalysis.DOM.txtUsernameOfLeague).val();

                combo = $(LeagueBetTypeAnalysis.DOM.ddlComboOfLeague).multiselect("getChecked").map(function () {
                    return this.value;
                }).get().join(',');

                if (combo == '') {
                    Validators.show($(LeagueBetTypeAnalysis.DOM.ddlComboOfLeague).multiselect("getButton").get(0), 'Please select your options');
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

                leagues = $(LeagueBetTypeAnalysis.DOM.ddlLeagueOfLeague).multiselect("getChecked").map(function () {
                    return this.value;
                }).get().join(',');

                if (leagues == '') {
                    Validators.show($(LeagueBetTypeAnalysis.DOM.ddlLeagueOfLeague).multiselect("getButton").get(0), 'Please select your options');
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
                LeagueBetTypeAnalysis.ResetCachedObjectOfBetType();
                $(LeagueBetTypeAnalysis.DOM.divResultOfBetType).html('');
                fromDate = $(LeagueBetTypeAnalysis.DOM.fromDateOfBetType).val();
                toDate = $(LeagueBetTypeAnalysis.DOM.toDateOfBetType).val();

                if (validator.validateDate(LeagueBetTypeAnalysis.DOM.fromDateOfBetType)) {
                    return;
                }

                if (validator.validateDate(LeagueBetTypeAnalysis.DOM.toDateOfBetType)) {
                    return;
                }

                combo = $(LeagueBetTypeAnalysis.DOM.ddlComboOfBetType).multiselect("getChecked").map(function () {
                    return this.value;
                }).get().join(',');
                if (combo == '') {
                    Validators.show($(LeagueBetTypeAnalysis.DOM.ddlComboOfBetType).multiselect("getButton").get(0), 'Please select your options');
                    return;
                }

                userName = $(LeagueBetTypeAnalysis.DOM.txtUsernameOfBetType).val();

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
                leagues = $(LeagueBetTypeAnalysis.DOM.ddlLeagueOfBetType).multiselect("getChecked").map(function () {
                    return this.value;
                }).get().join(',');

                if (leagues == '') {
                    Validators.show($(LeagueBetTypeAnalysis.DOM.ddlLeagueOfBetType).multiselect("getButton").get(0), 'Please select your options');
                    return;
                }
                if (leagueGroups == '-1') {
                    Validators.show($(LeagueBetTypeAnalysis.DOM.ddlGroupOfBetType).multiselect("getButton").get(0), 'Please select another option to get correct report!');
                }

                if (leagues.split(',').length > 1 && $(LeagueBetTypeAnalysis.DOM.ddlGroupOfBetType).multiselect("getButton").get(0).innerText != 'All') {
                    Validators.show($(LeagueBetTypeAnalysis.DOM.ddlLeagueOfBetType).multiselect("getButton").get(0), 'Please select single League to get correct report!');
                }
                betTypeTitle = $("#ddlBetType").multiselect("getButton").get(0).innerText;
            }

            $('#header').find(".sortable").each(function () {
                $(this).removeClass("headerSortDown").removeClass("headerSortUp");
                this.sortDirection = "";
            });

            LeagueBetTypeAnalysis.Data(combo,
                userName,
                parlayType,
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
                null,
                null,
                1,
                100);
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
            } else {
                $(LeagueBetTypeAnalysis.DOM.ddlLeague).multiselect("enable");
            }
            LeagueBetTypeAnalysis.ReloadLeagues(leagueGroups, LeagueBetTypeAnalysis.DOM.ddlLeague);
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
            } else {
                $(LeagueBetTypeAnalysis.DOM.ddlLeagueOfLeague).multiselect("enable");
            }
            LeagueBetTypeAnalysis.ReloadLeagues(leagueGroups, LeagueBetTypeAnalysis.DOM.ddlLeagueOfLeague);
        });

        $(LeagueBetTypeAnalysis.DOM.ddlGroupOfBetType).on('change', function () {
            var leagueGroups = $(LeagueBetTypeAnalysis.DOM.ddlGroupOfBetType).multiselect("getChecked").map(function () {
                return this.value;
            }).get().join(',');
            var leagues = $(LeagueBetTypeAnalysis.DOM.ddlLeagueOfBetType).multiselect("getChecked").map(function () {
                return this.value;
            }).get().join(',');
            if (LeagueBetTypeAnalysis.IsDisabled(2, leagueGroups, leagues)) {
                $(LeagueBetTypeAnalysis.DOM.ddlLeagueOfBetType).multiselect("disable");
            } else {
                $(LeagueBetTypeAnalysis.DOM.ddlLeagueOfBetType).multiselect("enable");
            }
            LeagueBetTypeAnalysis.ReloadLeagues(leagueGroups, LeagueBetTypeAnalysis.DOM.ddlLeagueOfBetType);
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
            } else {
                $(LeagueBetTypeAnalysis.DOM.ddlLeague).multiselect("enable");
            }
        });

        LeagueBetTypeAnalysis.GroupsAndLeagues(1,
            [LeagueBetTypeAnalysis.DOM.ddlGroup, LeagueBetTypeAnalysis.DOM.ddlGroupOfLeague, LeagueBetTypeAnalysis.DOM.ddlGroupOfBetType],
            [LeagueBetTypeAnalysis.DOM.ddlLeague, LeagueBetTypeAnalysis.DOM.ddlLeagueOfLeague, LeagueBetTypeAnalysis.DOM.ddlLeagueOfBetType],
            $("#fromDate").val(),
            $("#toDate").val());

        LeagueBetTypeAnalysis.BetTypes(1);

        $(LeagueBetTypeAnalysis.DOM.ddlSport).on('change', function () {
            LeagueBetTypeAnalysis.GroupsAndLeagues(parseInt($(this).val()), [LeagueBetTypeAnalysis.DOM.ddlGroup], [LeagueBetTypeAnalysis.DOM.ddlLeague], $("#fromDate").val(), $("#toDate").val());
        });

        $(LeagueBetTypeAnalysis.DOM.ddlSportOfLeague).on('change', function () {
            LeagueBetTypeAnalysis.GroupsAndLeagues(parseInt($(this).val()), [LeagueBetTypeAnalysis.DOM.ddlGroupOfLeague], [LeagueBetTypeAnalysis.DOM.ddlLeagueOfLeague], $("#fromDate-League").val(), $("#toDate-League").val());
        });

        $(LeagueBetTypeAnalysis.DOM.ddlSportOfBetType).on('change', function () {
            LeagueBetTypeAnalysis.GroupsAndLeagues(parseInt($(this).val()), [LeagueBetTypeAnalysis.DOM.ddlGroupOfBetType], [LeagueBetTypeAnalysis.DOM.ddlLeagueOfBetType], $("#fromDate-BetType").val(), $("#toDate-BetType").val());
            LeagueBetTypeAnalysis.BetTypes(parseInt($(this).val()));
        });

        $(document).on("click", '#header .sortable', function () {
            var data = {};
            switch ($("#tabs").tabs('option', 'active')) {
                case 0:
                    data = LeagueBetTypeAnalysis.cachedObject;
                    break;
                case 1:
                    data = LeagueBetTypeAnalysis.cachedObjectOfLeague;
                    break;
                default:
                    data = LeagueBetTypeAnalysis.cachedObjectOfBetType;
            };
            LeagueBetTypeAnalysis.SortData(this, data);
        });

        $(document).on('click', '.leagueTab', function () {
            $("#tabs").tabs("option", "active", 1);
            $(LeagueBetTypeAnalysis.DOM.txtHouseWLOfLeague).val('-1,000');
            var leagueId = this.getAttribute('data-leagueid');
            var leagueName = this.getAttribute('data-leaguename');
            var groupId = this.getAttribute('data-groupid');
            LeagueBetTypeAnalysis.HandleElementForLeagueTab(leagueId, groupId);
            LeagueBetTypeAnalysis.Data(LeagueBetTypeAnalysis.cachedObject.combo,
                LeagueBetTypeAnalysis.cachedObject.userName,
                LeagueBetTypeAnalysis.cachedObject.parlayType,
                LeagueBetTypeAnalysis.cachedObject.fromDate,
                LeagueBetTypeAnalysis.cachedObject.toDate,
                groupId,
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
            $(LeagueBetTypeAnalysis.DOM.txtHouseWLOfBetType).val('-1,000');
            var bettype = this.getAttribute('data-bettypeid');
            var bettypeName = this.getAttribute('data-bettypename');
            var parlayType = bettype == '38' ? '3' : LeagueBetTypeAnalysis.cachedObject.parlayType;
            LeagueBetTypeAnalysis.HandleElementForBetTypeTab(bettype);
            LeagueBetTypeAnalysis.Data(LeagueBetTypeAnalysis.cachedObject.combo,
                LeagueBetTypeAnalysis.cachedObject.userName,
                parlayType,
                LeagueBetTypeAnalysis.cachedObject.fromDate,
                LeagueBetTypeAnalysis.cachedObject.toDate,
                LeagueBetTypeAnalysis.cachedObject.leagueGroups,
                LeagueBetTypeAnalysis.cachedObject.leagues,
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
                : "Group [" + $(this).data('group') + "] - League [" + $(this).data('league') + "] - Event [" + $(this).data('event') + "] - [" + ($("#ddlBetType").find("option:checked").val() == 0 ? "Single Match Parlay" : $("#ddlBetType").find("option:checked").text()) + "]";
            LeagueBetTypeAnalysis.ExportExcelForWonTickets($(this).data('betteam'), $(this).data('match'), $(this).data('bettypeid'), title);
        });
    },

    SortData: function (element, data) {
        var currentObject = element;
        var currentColIndex = parseInt($(currentObject).attr("data-col-index"));

        if (currentColIndex ==data.sortById) {
            if (data.isSortAsc) {
                data.isSortAsc = false;
                $(element).removeClass("headerSortUp").addClass("headerSortDown");
            } else {
                data.isSortAsc = true;
                $(element).removeClass("headerSortDown").addClass("headerSortUp");
            }
        } else {
            if (currentObject.sortDirection == 'asc') {
                data.isSortAsc = true;
                $(element).removeClass("headerSortDown").addClass("headerSortUp");
            } else {
                data.isSortAsc = false;
                $(element).removeClass("headerSortUp").addClass("headerSortDown");
            }

            $(element).find(".sortable").each(function () {
                if ($(currentObject).index() != $(element).index()) {
                    $(element).removeClass("headerSortDown").removeClass("headerSortUp");
                    element.sortDirection = "";
                }
            });
        }

        if (!currentColIndex) {
            currentColIndex = $(currentObject).index();
        }

        data.sortById = parseInt(currentColIndex);
        LeagueBetTypeAnalysis.Data(data.combo,
            data.userName,
            data.parlayType,
            data.fromDate,
            data.toDate,
            data.leagueGroups,
            data.leagues,
            data.site,
            data.ratio,
            data.reportBy,
            data.sportType,
            data.betType,
            data.houseWL,
            data.isDetail,
            data.leagueTitle,
            data.betTypeTitle,
            data.sortById,
            data.isSortAsc,
            data.page,
            data.pageSize);
    },

    GroupsAndLeagues: function (sportType, groupElements, leagueElements, fromDate, toDate, callback) {
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
                LeagueBetTypeAnalysis.totalLeagueGroup = json.Groups.length;
                LeagueBetTypeAnalysis.LeagueList = json.Leagues;
                
                $.each(groupElements, function (index, group) {
                    LeagueBetTypeAnalysis.ReloadLeagueGroups(json.Groups, group);
                    var leagueGroups = $(group).multiselect("getChecked").map(function () {
                        return this.value;
                    }).get().join(',');
                    LeagueBetTypeAnalysis.ReloadLeagues(leagueGroups, leagueElements[index]);
                });
                if (callback !== undefined) {
                    callback();
                }
            },
            error: function (xhr, status, err) {
                alertBox.showError(xhr);
            },
            complete: function () {
                loading.hide();
            }
        });
    },

    BetTypes: function (sportType, callback) {
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
                if (callback !== undefined) {
                    callback();
                }
            },
            error: function (xhr, status, err) {
                alertBox.showError(xhr);
            },
            complete: function () {
                loading.hide();
            }
        });
    },

    Data: function (combo, userName, parlayType, fromDate, toDate, leagueGroups, leagues, site, ratio, reportBy, sportType, betType, houseWL, isDetail, leagueTitle, betTypeTitle, sortById, isSortAsc, page, pageSize) {
        var data = {
            combo: combo,
            userName: userName,
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
        var divResult = "";
        if (isDetail) {
            if (reportBy === 1) {
                LeagueBetTypeAnalysis.cachedObjectOfLeague = data;
                divResult = LeagueBetTypeAnalysis.DOM.divResultOfLeague;
            } else {
                LeagueBetTypeAnalysis.cachedObjectOfBetType = data;
                divResult = LeagueBetTypeAnalysis.DOM.divResultOfBetType;
            }
        } else {
            LeagueBetTypeAnalysis.cachedObject = data;
            divResult = LeagueBetTypeAnalysis.DOM.divResult;
        }

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
                if (isDetail) {
                    if (reportBy === 1) {
                        $(LeagueBetTypeAnalysis.DOM.divResultOfLeague).html(result);
                    } else {
                        $(LeagueBetTypeAnalysis.DOM.divResultOfBetType).html(result);
                    }
                } else {
                    $(LeagueBetTypeAnalysis.DOM.divResult).html(result);
                }
                
                $(divResult + ' #header').find(".sortable").each(function () {
                    if ($(this).attr("data-col-index") == sortById) {
                        if (isSortAsc) {
                            $(this).removeClass("headerSortDown").addClass("headerSortUp");
                        } else {
                            $(this).removeClass("headerSortUp").addClass("headerSortDown");
                        }
                    }
                });
                $(divResult +' #pageSize').on('change', function () {
                    if (isDetail) {
                        if (reportBy === 1) {
                            LeagueBetTypeAnalysis.HandlePaging(LeagueBetTypeAnalysis.cachedObjectOfLeague, parseInt($(this).val()));
                        } else {
                            LeagueBetTypeAnalysis.HandlePaging(LeagueBetTypeAnalysis.cachedObjectOfBetType, parseInt($(this).val()));
                        }
                    } else {
                        LeagueBetTypeAnalysis.HandlePaging(LeagueBetTypeAnalysis.cachedObject, parseInt($(this).val()));
                    }
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

    HandlePaging: function (data, pageSize) {
        data.pageSize = pageSize;
        data.page = 1;
        LeagueBetTypeAnalysis.Data(data.combo,
            data.userName,
            data.parlayType,
            data.fromDate,
            data.toDate,
            data.leagueGroups,
            data.leagues,
            data.site,
            data.ratio,
            data.reportBy,
            data.sportType,
            data.betType,
            data.houseWL,
            data.isDetail,
            data.leagueTitle,
            data.betTypeTitle,
            data.sortById,
            data.isSortAsc,
            data.page,
            data.pageSize);
    },

    ExportExcel: function (combo, userName, parlayType, fromDate, toDate, leagues, leagueGroups, site, ratio, reportBy, sportType, betType, houseWL, isDetail, leagueTitle, betTypeTitle) {

        var params = 'fromDate=' + fromDate + '&toDate=' + toDate + '&combo=' + combo + '&parlayType=' + parlayType + '&userName=' + userName
            + '&leagueGroups=' + leagueGroups + '&leagues=' + leagues + '&site=' + site + '&ratio=' + ratio + '&reportBy=' + reportBy + '&sportType=' + sportType
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
        var params = '';
        if ($("#tabs").tabs('option', 'active') === 1) {
            params = 'fromDate=' + LeagueBetTypeAnalysis.cachedObjectOfLeague.fromDate + '&toDate=' + LeagueBetTypeAnalysis.cachedObjectOfLeague.toDate
                + '&combo=' + LeagueBetTypeAnalysis.cachedObjectOfLeague.combo + '&userName=' + LeagueBetTypeAnalysis.cachedObjectOfLeague.userName
                + '&parlayType=' + LeagueBetTypeAnalysis.cachedObjectOfLeague.parlayType + '&leagueGroups=' + LeagueBetTypeAnalysis.cachedObjectOfLeague.leagueGroups
                + '&leagues=' + LeagueBetTypeAnalysis.cachedObjectOfLeague.leagues + '&site=' + LeagueBetTypeAnalysis.cachedObjectOfLeague.site
                + '&reportBy=3' + '&sportType=' + LeagueBetTypeAnalysis.cachedObjectOfLeague.sportType
                + '&betType=' + bettypeId + '&houseWL=' + LeagueBetTypeAnalysis.cachedObjectOfLeague.houseWL + '&betTeam=' + betTeam + '&match=' + match + "&title=" + title;
        } else {
            params = 'fromDate=' + LeagueBetTypeAnalysis.cachedObjectOfBetType.fromDate + '&toDate=' + LeagueBetTypeAnalysis.cachedObjectOfBetType.toDate
                + '&combo=' + LeagueBetTypeAnalysis.cachedObjectOfLeague.combo + '&userName=' + LeagueBetTypeAnalysis.cachedObjectOfLeague.userName
                + '&parlayType=' + LeagueBetTypeAnalysis.cachedObjectOfBetType.parlayType + '&leagueGroups=' + LeagueBetTypeAnalysis.cachedObjectOfBetType.leagueGroups
                + '&leagues=' + LeagueBetTypeAnalysis.cachedObjectOfBetType.leagues + '&site=' + LeagueBetTypeAnalysis.cachedObjectOfBetType.site
                + '&reportBy=3' + '&sportType=' + LeagueBetTypeAnalysis.cachedObjectOfBetType.sportType
                + '&betType=' + bettypeId + '&houseWL=' + LeagueBetTypeAnalysis.cachedObjectOfBetType.houseWL + '&betTeam=' + betTeam + '&match=' + match + "&title=" + title;
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
        xhttp.open("POST", resolveClientUrl('LeagueBetTypeAnalysis/ExportExcelForWonTickets'), true);
        xhttp.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
        xhttp.responseType = 'blob';
        loading.show();
        xhttp.send(params);
    },

    ResetCachedObject: function () {
        LeagueBetTypeAnalysis.cachedObject.fromDate = null;
        LeagueBetTypeAnalysis.cachedObject.toDate = null;
        LeagueBetTypeAnalysis.cachedObject.combo = null;
        LeagueBetTypeAnalysis.cachedObject.userName = null;
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

    ResetCachedObjectOfLeague: function () {
        LeagueBetTypeAnalysis.cachedObjectOfLeague.fromDate = null;
        LeagueBetTypeAnalysis.cachedObjectOfLeague.toDate = null;
        LeagueBetTypeAnalysis.cachedObjectOfLeague.combo = null;
        LeagueBetTypeAnalysis.cachedObjectOfLeague.userName = null;
        LeagueBetTypeAnalysis.cachedObjectOfLeague.parlayType = null;
        LeagueBetTypeAnalysis.cachedObjectOfLeague.leagueGroups = null;
        LeagueBetTypeAnalysis.cachedObjectOfLeague.leagues = null;
        LeagueBetTypeAnalysis.cachedObjectOfLeague.site = null;
        LeagueBetTypeAnalysis.cachedObjectOfLeague.ratio = null;
        LeagueBetTypeAnalysis.cachedObjectOfLeague.reportBy = 0;
        LeagueBetTypeAnalysis.cachedObjectOfLeague.sportType = 1;
        LeagueBetTypeAnalysis.cachedObjectOfLeague.betType = 0;
        LeagueBetTypeAnalysis.cachedObjectOfLeague.houseWL = 0;
        LeagueBetTypeAnalysis.cachedObjectOfLeague.sortById = null;
        LeagueBetTypeAnalysis.cachedObjectOfLeague.isSortAsc = null;
        LeagueBetTypeAnalysis.cachedObjectOfLeague.page = 1;
        LeagueBetTypeAnalysis.cachedObjectOfLeague.pageSize = 100;
    },

    ResetCachedObjectOfBetType: function () {
        LeagueBetTypeAnalysis.cachedObjectOfBetType.fromDate = null;
        LeagueBetTypeAnalysis.cachedObjectOfBetType.toDate = null;
        LeagueBetTypeAnalysis.cachedObjectOfBetType.combo = null;
        LeagueBetTypeAnalysis.cachedObjectOfBetType.userName = null;
        LeagueBetTypeAnalysis.cachedObjectOfBetType.parlayType = null;
        LeagueBetTypeAnalysis.cachedObjectOfBetType.leagueGroups = null;
        LeagueBetTypeAnalysis.cachedObjectOfBetType.leagues = null;
        LeagueBetTypeAnalysis.cachedObjectOfBetType.site = null;
        LeagueBetTypeAnalysis.cachedObjectOfBetType.ratio = null;
        LeagueBetTypeAnalysis.cachedObjectOfBetType.reportBy = 0;
        LeagueBetTypeAnalysis.cachedObjectOfBetType.sportType = 1;
        LeagueBetTypeAnalysis.cachedObjectOfBetType.betType = 0;
        LeagueBetTypeAnalysis.cachedObjectOfBetType.houseWL = 0;
        LeagueBetTypeAnalysis.cachedObjectOfBetType.sortById = null;
        LeagueBetTypeAnalysis.cachedObjectOfBetType.isSortAsc = null;
        LeagueBetTypeAnalysis.cachedObjectOfBetType.page = 1;
        LeagueBetTypeAnalysis.cachedObjectOfBetType.pageSize = 100;
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
        html += '<option value="0"></option>';
        $('#ddlBetType').html(html);
        $('#ddlBetType').multiselect('refresh');
    },

    IsDisabled: function (reportBy, leagueGroups, leagues) {
        return leagueGroups.split(',').length == LeagueBetTypeAnalysis.totalLeagueGroup
            && reportBy == '2' && $("#tabs").tabs('option', 'active') !== 1;
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

    HandleElementForLeagueTab: function (leagueId, groupId) {
        $(LeagueBetTypeAnalysis.DOM.fromDateOfLeague).datepicker("setDate", LeagueBetTypeAnalysis.cachedObject.fromDate);
        $(LeagueBetTypeAnalysis.DOM.toDateOfLeague).datepicker("setDate", LeagueBetTypeAnalysis.cachedObject.toDate);
        $(LeagueBetTypeAnalysis.DOM.ddlSportOfLeague).val(LeagueBetTypeAnalysis.cachedObject.sportType).multiselect("refresh");
        $(LeagueBetTypeAnalysis.DOM.ddlSiteOfLeague).val(LeagueBetTypeAnalysis.cachedObject.site).multiselect("refresh");
        $(LeagueBetTypeAnalysis.DOM.ddlParlayTypeOfLeague).val(LeagueBetTypeAnalysis.cachedObject.parlayType.split(',')).multiselect("refresh");
        $(LeagueBetTypeAnalysis.DOM.ddlComboOfLeague).val(LeagueBetTypeAnalysis.cachedObject.combo.split(',')).multiselect("refresh");
        $(LeagueBetTypeAnalysis.DOM.txtUsernameOfLeague).val(LeagueBetTypeAnalysis.cachedObject.userName);
        LeagueBetTypeAnalysis.GroupsAndLeagues(LeagueBetTypeAnalysis.cachedObject.sportType,
            [LeagueBetTypeAnalysis.DOM.ddlGroupOfLeague],
            [LeagueBetTypeAnalysis.DOM.ddlLeagueOfLeague],
            LeagueBetTypeAnalysis.cachedObject.fromDate,
            LeagueBetTypeAnalysis.cachedObject.toDate, function () {
                $(LeagueBetTypeAnalysis.DOM.ddlGroupOfLeague).val([groupId]).multiselect("refresh");
                LeagueBetTypeAnalysis.ReloadLeagues(groupId, LeagueBetTypeAnalysis.DOM.ddlLeagueOfLeague)
                if (leagueId > 0) {
                    $(LeagueBetTypeAnalysis.DOM.ddlLeagueOfLeague).val([leagueId]).multiselect("refresh");
                };
                if (LeagueBetTypeAnalysis.IsDisabled(1,groupId, leagueId)) {
                    $(LeagueBetTypeAnalysis.DOM.ddlLeagueOfLeague).multiselect("disable");
                } else {
                    $(LeagueBetTypeAnalysis.DOM.ddlLeagueOfLeague).multiselect("enable");
                }
        });
    },

    HandleElementForBetTypeTab: function (bettype) {
        $(LeagueBetTypeAnalysis.DOM.fromDateOfBetType).datepicker("setDate", LeagueBetTypeAnalysis.cachedObject.fromDate);
        $(LeagueBetTypeAnalysis.DOM.toDateOfBetType).datepicker("setDate", LeagueBetTypeAnalysis.cachedObject.toDate);
        $(LeagueBetTypeAnalysis.DOM.ddlSportOfBetType).val(LeagueBetTypeAnalysis.cachedObject.sportType).multiselect("refresh");
        $(LeagueBetTypeAnalysis.DOM.ddlComboOfBetType).val(LeagueBetTypeAnalysis.cachedObject.combo.split(',')).multiselect("refresh");
        $(LeagueBetTypeAnalysis.DOM.txtUsernameOfBetType).val(LeagueBetTypeAnalysis.cachedObject.userName);
        LeagueBetTypeAnalysis.BetTypes(LeagueBetTypeAnalysis.cachedObject.sportType, function () {
            if (bettype == '38') {
                $("#ddlBetType").val('0').multiselect("refresh");
            } else {
                $("#ddlBetType").val(bettype).multiselect("refresh");
            }
        });
        $(LeagueBetTypeAnalysis.DOM.ddlSiteOfBetType).val(LeagueBetTypeAnalysis.cachedObject.site).multiselect("refresh");
        if (bettype == '38') {
            $(LeagueBetTypeAnalysis.DOM.ddlParlayTypeOfBetType).val(['3']).multiselect("refresh");
        } else {
            $(LeagueBetTypeAnalysis.DOM.ddlParlayTypeOfBetType).val(LeagueBetTypeAnalysis.cachedObject.parlayType.split(',')).multiselect("refresh");
        }
        LeagueBetTypeAnalysis.GroupsAndLeagues(LeagueBetTypeAnalysis.cachedObject.sportType,
            [LeagueBetTypeAnalysis.DOM.ddlGroupOfBetType],
            [LeagueBetTypeAnalysis.DOM.ddlLeagueOfBetType],
            LeagueBetTypeAnalysis.cachedObject.fromDate,
            LeagueBetTypeAnalysis.cachedObject.toDate, function () {
                $(LeagueBetTypeAnalysis.DOM.ddlGroupOfBetType).val(LeagueBetTypeAnalysis.cachedObject.leagueGroups.split(',')).multiselect("refresh");
                LeagueBetTypeAnalysis.ReloadLeagues(LeagueBetTypeAnalysis.cachedObject.leagueGroups, LeagueBetTypeAnalysis.DOM.ddlLeagueOfBetType);
                $(LeagueBetTypeAnalysis.DOM.ddlLeagueOfBetType).val(LeagueBetTypeAnalysis.cachedObject.leagues.split(',')).multiselect("refresh");
                if (LeagueBetTypeAnalysis.IsDisabled(2, LeagueBetTypeAnalysis.cachedObject.leagueGroups, LeagueBetTypeAnalysis.cachedObject.leagues)) {
                    $(LeagueBetTypeAnalysis.DOM.ddlLeagueOfBetType).multiselect("disable");
                } else {
                    $(LeagueBetTypeAnalysis.DOM.ddlLeagueOfBetType).multiselect("enable");
                }
            });
    }
}

$(document).ready(function () {
    LeagueBetTypeAnalysis.Init();
});

function MovePage(pageNumber) {
    switch ($("#tabs").tabs('option', 'active')) {
        case 0:
            LeagueBetTypeAnalysis.Data(LeagueBetTypeAnalysis.cachedObject.combo,
                LeagueBetTypeAnalysis.cachedObject.userName,
                LeagueBetTypeAnalysis.cachedObject.parlayType,
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
            break;
        case 1:
            LeagueBetTypeAnalysis.Data(LeagueBetTypeAnalysis.cachedObjectOfLeague.combo,
                LeagueBetTypeAnalysis.cachedObjectOfLeague.userName,
                LeagueBetTypeAnalysis.cachedObjectOfLeague.parlayType,
                LeagueBetTypeAnalysis.cachedObjectOfLeague.fromDate,
                LeagueBetTypeAnalysis.cachedObjectOfLeague.toDate,
                LeagueBetTypeAnalysis.cachedObjectOfLeague.leagueGroups,
                LeagueBetTypeAnalysis.cachedObjectOfLeague.leagues,
                LeagueBetTypeAnalysis.cachedObjectOfLeague.site,
                LeagueBetTypeAnalysis.cachedObjectOfLeague.ratio,
                LeagueBetTypeAnalysis.cachedObjectOfLeague.reportBy,
                LeagueBetTypeAnalysis.cachedObjectOfLeague.sportType,
                LeagueBetTypeAnalysis.cachedObjectOfLeague.betType,
                LeagueBetTypeAnalysis.cachedObjectOfLeague.houseWL,
                LeagueBetTypeAnalysis.cachedObjectOfLeague.isDetail,
                LeagueBetTypeAnalysis.cachedObjectOfLeague.leagueTitle,
                LeagueBetTypeAnalysis.cachedObjectOfLeague.betTypeTitle,
                LeagueBetTypeAnalysis.cachedObjectOfLeague.sortById,
                LeagueBetTypeAnalysis.cachedObjectOfLeague.isSortAsc,
                pageNumber,
                LeagueBetTypeAnalysis.cachedObjectOfLeague.pageSize);
            break;
        default:
            LeagueBetTypeAnalysis.Data(LeagueBetTypeAnalysis.cachedObjectOfBetType.combo,
                LeagueBetTypeAnalysis.cachedObjectOfBetType.userName,
                LeagueBetTypeAnalysis.cachedObjectOfBetType.parlayType,
                LeagueBetTypeAnalysis.cachedObjectOfBetType.fromDate,
                LeagueBetTypeAnalysis.cachedObjectOfBetType.toDate,
                LeagueBetTypeAnalysis.cachedObjectOfBetType.leagueGroups,
                LeagueBetTypeAnalysis.cachedObjectOfBetType.leagues,
                LeagueBetTypeAnalysis.cachedObjectOfBetType.site,
                LeagueBetTypeAnalysis.cachedObjectOfBetType.ratio,
                LeagueBetTypeAnalysis.cachedObjectOfBetType.reportBy,
                LeagueBetTypeAnalysis.cachedObjectOfBetType.sportType,
                LeagueBetTypeAnalysis.cachedObjectOfBetType.betType,
                LeagueBetTypeAnalysis.cachedObjectOfBetType.houseWL,
                LeagueBetTypeAnalysis.cachedObjectOfBetType.isDetail,
                LeagueBetTypeAnalysis.cachedObjectOfBetType.leagueTitle,
                LeagueBetTypeAnalysis.cachedObjectOfBetType.betTypeTitle,
                LeagueBetTypeAnalysis.cachedObjectOfBetType.sortById,
                LeagueBetTypeAnalysis.cachedObjectOfBetType.isSortAsc,
                pageNumber,
                LeagueBetTypeAnalysis.cachedObjectOfBetType.pageSize);
    }
    
}

