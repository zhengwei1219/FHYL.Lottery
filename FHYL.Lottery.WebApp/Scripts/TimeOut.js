// JavaScript Document

var fp,fg,iqihao="",oli='',statetime=5000,opentime;
//翻页变量
var curPage = 1; //当前页码
var total, pageSize, totalPage; //总记录数，每页显示数，总页数
var nextExp;

function getdata(lotteryType) {
    clearTimeout(fg);
    $.ajax({
        type: 'POST',
        url: getdataUrl,
        data:{lotteryType:lotteryType},
        dataType: "json",
        success: function (data) {
            if (data == null) {
                fg = setTimeout(function () { getdata(lotteryType); }, 3000);
                return;
            }
            //$('#getdatacount').append(data.Qihao);
            openTime = data.Rema;
            nextExp = data.NextExpect;
            if (data.Expect != currExp) {
                if (data.OpenCode == "") data.OpenCode = "正在开奖";
                if (lotteryType == "广东快乐十分" || lotteryType == "重庆时时彩") {
                    $('#CurrentExInfo').html(data.LotteryType + " " + data.Expect.substring(2) + "期开奖");
                    $('#nextExp').html("距离 " + data.NextExpect.substring(2) + "期");
                }
                else {
                    $('#CurrentExInfo').html(data.LotteryType + " " + data.Expect + "期开奖");
                    $('#nextExp').html("距离 " + data.NextExpect + "期");
                }
                $('#CurrentCodeInfo').html(data.OpenCode);
                
                currExp = data.Expect;

            }
            else {
                if(data.OpenCode != "")
                    $('#CurrentCodeInfo').html(data.OpenCode);
            }
            getautoTime(lotteryType);
            fg = setTimeout(function () { getdata(lotteryType); }, 3000);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            //alert("加载失败：" + errorThrown);
        }
    });
	
}
function getautoTime(lotteryType) {
        clearTimeout(fp);
        //$('#opentime').html(openTime);
    //提前两分钟封盘
        if(openTime>=120){
            endtime(openTime - 120);
            ContorlButton(false, lotteryType);
            ContorlButtonPay(false, lotteryType);
        } else {
            endtime(0);
            if (typeof RecAll != "undefined") {
                RecAll();
            }
            ContorlButton(true, lotteryType);
            ContorlButtonPay(true, lotteryType);
        }
        if (openTime <= 0 && lotteryType=="六合彩")
            $('#CurrentCodeInfo').html('正在开奖');
        openTime--;
        fp = setTimeout(function () { getautoTime(lotteryType); }, 1000);
    }
    function endtime(iTime){
        if(iTime<0){
            iTime=0;
        }
        //if(iTime>20)iTime=iTime-20;
        var hh,mm,ss;
        hh = parseInt((iTime/60/60));
        mm = parseInt((iTime/60)%60);
        ss = parseInt(iTime%60);
        if(hh<10)hh = '0'+hh;
        if(mm<10)mm = '0'+mm;
        if(ss<10)ss = '0'+ss;
        $('#txtTimer').html("截止投注: "+ hh + ' : ' + mm + ' : ' + ss);
    }

    function ContorlButton(disable, lotteryType) {
        btns = $(".dlt_xh").find("a");
        if (btns != undefined) {
            btns.unbind("click");
            if (!disable) {

                btns.click(function () {
                    $(this).toggleClass("selected");
                    $("#dv_buyed").html("已选择 " + SumBuy() + " 注 " + SumBuyMoney() + " 元");
                });
            }
            else
            {
                btns.click(function () {
                    alert("本期已封盘，请等待下一期");
                    return;
                });
            }
        }
        if ($('#dv_pay') != undefined) {
            $('#dv_pay').unbind("click");
            if (!disable) {
                $('#dv_pay').click(function () {
                    var selectNumbers = "";
                    $(".selected").each(function (k, v) {

                        var selNum = $(this).text();
                        var selCode = $(this).parent().attr("id");
                        selectNumbers += selCode + "|" + selNum + ",";

                    });
                    if (selectNumbers == "") {
                        alert("请至少选择一注");
                        return;
                    }
                    var urlstr = "/Buy/Prepay?selectNumbers=" + selectNumbers + "&lotteryType=" + lotteryType;
                    window.location.href = encodeURI(urlstr);
                });
            }
            else {

            
                $('#dv_pay').click(function () {
                    alert("本期已封盘，请等待下一期");
                    return;
                });

            }
        }
        
        
            
        
        
    }
    function ContorlButtonPay(disable, lotteryType) {
        if (location.href.indexOf("Prepay") < 0) return;
        if ($("#dv_ConfirmPay") == null || $("#dv_ConfirmPay") == "undefined")
            return;
        $("#dv_ConfirmPay").unbind("click");
        if (!disable) {
            //确认下注
            $("#dv_ConfirmPay").click(function () {
                var payCountVal = $("#payCount").val();//下注的倍数
                var payCount = payCountVal.replace(/(^\s*)|(\s*$)/g, "");
                var reg = /^([1-9][0-9]*){1,3}$/
                if (!reg.test(payCount)) {
                    alert("投注倍数输入的数据有误！");
                    return;
                }
                $("#times").text(payCountVal);
                var betMoney = parseInt('@ViewBag.count') * parseInt(payCountVal);
                //较验余额
                var balance = '@ViewBag.blanace';

                if (parseInt(betMoney) > parseInt(balance)) {
                    alert("余额不足，请充值！");
                }
                
                $.post("/Buy/Pay", { payCount: payCount, selectNumbers: selectNumbers, lotteryType: lotteryType, expect: nextExp }, function (data) {
                    if (data == "ok") {
                        alert("下注成功！");
                        if (lotteryType == "广东快乐十分")
                            location.href = "/buy/Klsf";
                        else if (lotteryType == "广东11选5")
                            location.href = "/buy/gd11x5";
                        else if (lotteryType == "重庆时时彩")
                            location.href = "/buy/ssc";
                        else if (lotteryType == "六合彩")
                            location.href = "/buy/lhc";
                    } else {
                        alert(data);
                    }
                });
            });
        }
        else {
            alert("本期已封盘，自动返回进行下一期投注");
            if (lotteryType == "广东快乐十分")
                location.href = "/buy/Klsf";
            else if(lotteryType == "广东11选5")
                location.href = "/buy/gd11x5";
            else if (lotteryType == "重庆时时彩")
                location.href = "/buy/ssc";
            else if (lotteryType == "六合彩")
                location.href = "/buy/lhc";
        }
    }
    
    