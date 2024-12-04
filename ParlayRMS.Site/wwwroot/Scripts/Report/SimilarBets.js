$(function () {
    $("#btnSearch").addClass("button");

    $("#betId").keypress(function (e) {
        switch (e.keyCode) {
            case 13: // enter
                $("#btnSearch").click();
                break;
        }
    });

    $('#btnSearch').click(function () {
        var transId = $.trim($('#betId').val().replace(/,/g, ''));
        if (transId == '') {
            Validators.show($('#betId').get(0), 'This field is required');
            return;
        }

        if (!isNumber(transId) || parseInt(transId) < 0) {
            Validators.show($("#betId").get(0), 'BetID must be greater or than 0');
            return;
        }

        var isSimilar = $("#similar").is(":checked");

        var data = {
            TransId: transId,
            IsSimilar: isSimilar
        }
        var container = $('#divResult');

        $.ajax({
            url: resolveClientUrl('SimilarBets/Data'),
            type: 'POST',
            data: data,
            dataType: 'html',
            cache: false,
            beforeSend: function (xhr) {
                loading.show();
            },
            success: function (result) {
                container.html(result);
                var table = $('#similarbetlist').find('table');
                var theadTable = table.filter('.thead');
                var tbodyTable = table.filter('.tbody');
                tbodyTable.children('tbody').children("tr:even").addClass("even");
                tableSorter.bind2(theadTable, tbodyTable, {
                    type: 'float',
                    onFinish: function () {
                        var trs = tbodyTable.children('tbody').children("tr");
                        trs.removeClass("even");
                        trs.filter(':even').addClass("even");
                        trs.each(function (index, tr) {
                            $(tr).children('td:first').text(index + 1);
                        });
                    }
                });
                checkAll();
            },
            error: function () {
                var messageError = "<div class='error'>Ticket " + transId + " is not found</div>";
                container.html(messageError);
            },
            complete: function () {
                container.show();
                loading.hide();
            }
        });
    });

    if ($('#betId').val() != '') {
        $('#btnSearch').click();
    }
});

function ViewSimilarBets() {
    if ($('#similarbetlist').is(":visible")) {
        $('#similarbetlist').hide();
    }
    else {
        $('#similarbetlist').hide().slideToggle("slow");
    }
}

function CloseSimilarBets() {
    $('#similarbetlist').hide();
}

function OpenViewIpInfo(ip, transDate) {
    const url = resolveClientUrl('ViewIPInfo?ip=' + ip + '&date=' + transDate);
    window.open(url, "_blank");
}

function ViewBetDetails(username, transId, originalTransId, refNo, originalRefNo, custId, originalCustId, betType) {
    if (betType === 38) {
        ViewSingleMatchParlayDetails(transId, originalTransId, custId, originalCustId, username);
    } else if (betType === 376) {
        ViewPenaltyShootoutParlayDetails(transId, username);
    }
    else {
        ViewSystemParlayDetails(username, transId, originalTransId, refNo, originalRefNo);
    }
}

function ViewSystemParlayDetails(username, transId, originalTransId, refNo, originalRefNo) {
    var data = {
        TransId: transId,
        OriginalTransId: originalTransId,
        Username: username,
        RefNo: refNo,
        OriginalRefNo: originalRefNo
    }

    $.ajax({
        url: resolveClientUrl('SimilarBets/BetDetails'),
        type: 'POST',
        data: data,
        dataType: 'html',
        cache: false,
        beforeSend: function () {
            loading.show();
        },
        success: function (data) {
            popup.show(data);
            $(".betdetails").css('overflow-y', 'auto');
            $("#simplemodal-container").width($("#simplemodal-container").width() + 20);
        },
        error: function (xhr, status, err) {
            alertBox.showError(xhr);
        },
        complete: function () {
            loading.hide();
        }
    });
}

function ViewSingleMatchParlayDetails(transId, originalTransId, custId, originalCustId, username) {
    var data = {
        TransId: transId,
        OriginalTransId: originalTransId,
        CustId: custId,
        OriginalCustId: originalCustId,
        Username: username
    }

    $.ajax({
        url: resolveClientUrl('SimilarBets/SingleMatchParlayDetailPopup'),
        type: 'POST',
        data: data,
        dataType: 'html',
        cache: false,
        beforeSend: function () {
            loading.show();
        },
        success: function (result) {
            modalPopup(result);
            $(".betdetails").css('overflow-y', 'auto');
            $("#simplemodal-container").width($("#simplemodal-container").width() + 20);
        },
        error: function (xhr, status, err) {
            alertBox.showError(xhr.responseText);
        },
        complete: function () {
            loading.hide();
        }
    });
}

function ViewPenaltyShootoutParlayDetails(transId, username) {
    var data = {
        TransId: transId,
        Username: username
    }

    $.ajax({
        url: resolveClientUrl('BetList/PenaltyShootoutParlayDetailPopup'),
        type: 'POST',
        data: data,
        dataType: 'html',
        cache: false,
        beforeSend: function () {
            loading.show();
        },
        success: function (result) {
            modalPopup(result);
            $(".betdetails").css('overflow-y', 'auto');
            $("#simplemodal-container").width($("#simplemodal-container").width() + 20);
        },
        error: function (xhr, status, err) {
            alertBox.showError(xhr.responseText);
        },
        complete: function () {
            loading.hide();
        }
    });
}

function updateCheckedUser(element, userid, username, transid, isAll) {
    if (isAll) {
        confirmWithFullEvent('Confirm', 'Are you sure you reviewed all tickets?', function () {
            var data = {
                TransId: getTransIds(),
                Username: username,
                UserId: userid
            }

            $.ajax({
                url: resolveClientUrl('SimilarBets/CheckBy'),
                type: 'POST',
                data: data,
                dataType: 'html',
                cache: false,
                beforeSend: function () {
                    loading.show();
                },
                success: function (result) {
                    var json = $.parseJSON(result);
                    switch (json.StatusId) {
                        case 0:
                            disabledandCheckedCheckBox("#cbTrackingUser");
                            reUpdateCheckedUser(username, false);
                            break;
                        case 1:
                            disabledandCheckedCheckBox("#cbTrackingUser");
                            reUpdateCheckedUser(json.Message, true);
                            break;
                        case 2:
                            $("#cbTrackingUser").prop("checked", false);
                            alertBox.showErrorMessage(json.Message);
                            break;
                    }
                },
                error: function (xhr, status, err) {
                    alertBox.showError(xhr.responseText);
                },
                complete: function () {
                    loading.hide();
                }
            });
        }, function () {
            $("#cbTrackingUser").prop("checked", false);
        });
    } else {
        var data = {
            TransId: transid,
            Username: username,
            UserId: userid
        }

        $.ajax({
            url: resolveClientUrl('SimilarBets/CheckBy'),
            type: 'POST',
            data: data,
            dataType: 'html',
            cache: false,
            beforeSend: function () {
                loading.show();
            },
            success: function (result) {
                var json = $.parseJSON(result);
                switch (json.StatusId) {
                    case 0:
                        disabledandCheckedCheckBox(element);
                        $(element + "-text").text(username);
                        checkAll();
                        break;
                    case 1:
                        alertBox.show("Information", "This ticket has been already checked by another user");
                        disabledandCheckedCheckBox(element);
                        var object = json.Message.split(',')[0];
                        $(element + "-text").text(object.split(':')[1]);
                        checkAll();
                        break;
                    case 2:
                        $(element).prop("checked", false);
                        alertBox.showErrorMessage(json.Message);
                        break;
                }
            },
            error: function (xhr, status, err) {
                alertBox.showError(xhr.responseText);
            },
            complete: function () {
                loading.hide();
            }
        });
    }
}

function getTransIds() {
    var transids = '';
    $(".cbTrackingUser-item").each(function (inx, element) {
        if (!element.disabled) {
            transids += element.getAttribute("data-transid") + ',';
        }
    });
    return transids;
}

function disabledandCheckedCheckBox(element) {
    $(element).prop("checked", true);
    $(element).prop("disabled", true);
}

function reUpdateCheckedUser(object, isUserList) {
    if (isUserList) {
        var list = object.split(',');
        $.each(list, function (inx, val) {
            var userInfo = val.split(':');
            $(".cbTrackingUser-item").each(function (inx, element) {
                if (!element.disabled) {
                    var transid = element.getAttribute("data-transid");
                    if (transid === userInfo[0]) {
                        element.disabled = true;
                        element.checked = true;
                        var labelId = "#" + element.getAttribute("id") + "-text";
                        $(labelId).text(userInfo[1]);
                    }
                }
            });
        });
    } else {
        $(".cbTrackingUser-item").each(function (inx, element) {
            if (!element.disabled) {
                element.disabled = true;
                element.checked = true;
                var labelId = "#" + element.getAttribute("id") + "-text";
                $(labelId).text(object);
            }
        });
    }
}

function checkAll() {
    var isCheckedAll = true;
    $(".cbTrackingUser-item").each(function (inx, element) {
        if (!element.checked) {
            isCheckedAll = false;
            return false;
        }
    });

    if (isCheckedAll) {
        $("#cbTrackingUser").prop("checked", true);
        $("#cbTrackingUser").prop("disabled", true);
    }
}