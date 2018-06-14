function RecAll() {
    $(".selected").each(function (k, v) {
        $(this).removeAttr("class");
        $("#dv_buyed").html("已选择 " + SumBuy() + " 注 " + SumBuyMoney() + " 元");

    });
}