$(document).ready(function () {
    $('.updatehouse').hide();
    $('.updateBalance').hide();
    $('.updateHouseNameBtn').click(function () {
        $('.updateHouseName').show();
    });
    $('.updateHouseStreetBtn').click(function () {
        $('.updateHouseStreet').show();
    });
    $('.updateHouseInfoBtn').click(function () {
        $('.updateHouseInfo').show();
    });
    $('.updateHousePriceBtn').click(function () {
        $('.updateHousePrice').show();
    });
    $('.updateHouseTypeBtn').click(function () {
        $('.updateHouseType').show();
    });
    $('.updateHouseImageBtn').click(function () {
        $('.updateHouseImage').show();
    });
    $('.updateBalanceBtn').click(function () {
        $('.updateBalance').show();
    });
});
function loadComments(id) {
    $.get('/Comments/houseId=' + id).done(function (data) {
        for (let com of data) {
            $('.comments-list').append('<div class="media row">' +
                '<div class="col-md-3">' +
                '<p class="pull-right lead"><small>' + com.leavedAtString + '</small></p>' +
                '<span class="media-left">' +
                    '<i class="fa fa-user" style="font-size: 24px;"></i>' +
                '</span></div>' +
                '<div class="media-body col-md-9">' +
                    '<h4 class="media-heading user_name">' + com.user.owner.user.login + '</h4>' +
                    '<p class="comment-text">' + com.text + '</p>' +
                '</div></div>');
        }
    });
}
function loadFilterPrices() {
    $.get(
        '/housing/houses/maxprice'
    ).done(function (maxPrice) {
        $.get('/housing/houses/minprice').done(function (minPrice) {
            let price = minPrice;
            while (price < maxPrice) {
                $('#filterPrice').append('<option value="' + Math.round(Number(price + price + 5000000) / 2) + '">' + price + ' - ' + Number(price + 5000000) + 'KZT</option>');
                price += 5000000;
            }
        });
    });
}