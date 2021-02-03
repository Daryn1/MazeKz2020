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

function checkCartHouse(ownerId, houseId) {
    $.ajax({
        type: "GET",
        url: '/CartHouse/ownerId=' + ownerId + '/houseId=' + houseId,
        statusCode: {
            200: function(){
                $('.carthouse').append('<form class="row" style="justify-content: flex-end;" method="post" action="/CartHouse/ownerid=' + ownerId + '/houseId=' + houseId + '/delete">' +
                    '<div style="margin-right: 20px;"><i class="fa fa-heart" style="color: crimson; font-size: 2em;"></i>В избранном</div>' +
                    '<button type="submit" class="btn btn-outline-danger">' +
                    '<i style="font-size: 30px;" class="fa">&#xf014;</i></button> ' +
                    '</form>');
            },
            404: function(){
                $('.carthouse').append('<form method="post" action="/CartHouse/ownerid=' + ownerId + '/houseId=' + houseId + '/add">' +
                    '<button type="submit" class="btn btn-outline-success">Добавить в избранное</button>' +
                    '</form>');
            }
        }
    });
}


function appendOwnerRequestForms(ownerId, houseId) {
    $.ajax({
        type: 'GET',
        url: '/housingownerrequests/ownerId=' + ownerId + '/houseId=' + houseId,
        statusCode: {
            404: function () {
                $('.houseRequests').append('<button style="margin: 20px 0;" type="button" class="btn ownerRequestBtn btn-outline-primary">Запрос на покупку</button>' +
                    '<form class="sendOwnerRequest">' +
                    '<div class="form-row">' +
                    '<textarea class="form-control extraOwnerInfo" style="margin-top: 20px;" rows="3" placeholder="Дополнительная информация о покупке"></textarea></div>' +
                    '<div class="form-row" style="text-align: right; margin-top: 20px;"><button type="submit" class="btn btn-primary">Отправить запрос</button></div>' +
                    '</form>');
                $('.sendOwnerRequest').hide();
                $('.ownerRequestBtn').click(function () {
                    $('.sendOwnerRequest').show();
                });
                $('.sendOwnerRequest').submit(function (e) {
                    e.preventDefault();
                    let info = $('.extraOwnerInfo').val();
                    if (!info)
                        $('.houseRequestError').html('Заполните поле');
                    else {
                        let request = {
                            ExtraInfo: info,
                            OwnerId: ownerId,
                            HouseId: houseId
                        };
                        $.ajax({
                            type: "POST",
                            url: '/housingownerrequests/add',
                            data: JSON.stringify(request),
                            contentType: 'application/json',
                            success: function (data) {
                                location.reload();
                            },
                            error: function (data) {
                                $('.houseRequestError').html(data.responseText);
                            }
                        });
                    }
                });
            },
            200: function () {
                $('.houseRequests').append('<form style="margin: 20px 0;" class="deleteOwnerRequest">' +
                    '<div class="form-row alert alert-success" role="alert">' +
                    '<label for="deleteOwnerRequest">Отправлен запрос на покупку</label><div style="text-align: right;"><button type="submit" id="deleteOwnerRequest"' +
                    'class="btn btn-outline-danger d-inline">Удалить запрос</button></div></div>' +
                    '</form>');
                $('.deleteOwnerRequest').submit(function (e) {
                    e.preventDefault();
                        $.ajax({
                            type: "DELETE",
                            url: '/housingownerrequests/ownerId=' + ownerId + '/houseId=' + houseId + '/delete',
                            success: function (data) {
                                location.reload();
                            },
                            error: function (data) {
                                $('.houseRequestError').html(data.responseText);
                            }
                        });
                });
            }
        }
    });
}
function appendResidentRequestForms(residentId, houseId) {
    $.ajax({
        type: 'GET',
        url: '/housingresidentrequests/residentId=' + residentId + '/houseId=' + houseId,
        statusCode: {
            404: function () {
                $('.houseRequests').append('<button style="margin: 20px 0;" type="button" class="btn residentRequestBtn btn-outline-primary">Запрос на жительство</button>' +
                    '<form class="sendResidentRequest">' +
                    '<div class="form-row">' +
                    '<textarea class="form-control extraResidentInfo" rows="3" placeholder="Дополнительная информация о жительстве"></textarea></div>' +
                    '<div class="form-row"><button type="submit" style="margin-top: 20px;" class="btn btn-primary">Отправить запрос</button></div>' +
                    '</form>');
                $('.sendResidentRequest').hide();
                $('.residentRequestBtn').click(function () {
                    $('.sendResidentRequest').show();
                });
                $('.sendResidentRequest').submit(function (e) {
                    e.preventDefault();
                    let info = $('.extraResidentInfo').val();
                    if (!info)
                        $('.houseRequestError').html('Заполните поле');
                    else {
                        let request = {
                            ExtraInfo: info,
                            ResidentId: residentId,
                            HouseId: houseId
                        };
                        $.ajax({
                            type: "POST",
                            url: '/housingresidentrequests/add',
                            data: JSON.stringify(request),
                            contentType: 'application/json',
                            success: function (data) {
                                location.reload();
                            },
                            error: function (data) {
                                $('.houseRequestError').html(data.responseText);
                            }
                        });
                    }
                });
            },
            200: function () {
                $('.houseRequests').append('<form style="margin: 20px 0;" class="deleteResidentRequest">' +
                    '<div class="form-row alert alert-success" role="alert">' +
                    '<label for="deleteResidentRequest">Отправлен запрос на жительство</label><div style="text-align: right;"><button type="submit" id="deleteResidentRequest"' +
                    'class="btn btn-outline-danger d-inline">Удалить запрос</button></div></div>' +
                    '</form>');
                $('.deleteResidentRequest').submit(function (e) {
                    e.preventDefault();
                    $.ajax({
                        type: "DELETE",
                        url: '/housingresidentrequests/residentId=' + residentId + '/houseId=' + houseId + '/delete',
                        success: function (data) {
                            location.reload();
                        },
                        error: function (data) {
                            $('.houseRequestError').html(data.responseText);
                        }
                    });
                });
            }
        }
    });
}

function loadOwnerRequests(houseId) {
    $.get('/housingownerrequests/houseId=' + houseId).done(function (data) {
        for (let req of data) {
            $('.ownersRequests').append('<a class="dropdown-item" href="#"><div class="row"><div class="col-md-10">' +
                '<h5>' + req.owner.user.login + '</h5>' +
                '<p class="lead">' + req.extraInfo + '</p></div><form method="post" action="/housingownerrequests/ownerId=' + req.ownerId + '/houseId=' + houseId +'/apply" class="col-md-2"><button class="btn btn-outline-success">&#9989;</button></form></div>' +
                '</a>');
        }
    });
}
function loadResidentRequests(houseId) {
    $.get('/housingresidentrequests/houseId=' + houseId).done(function (data) {
        for (let req of data) {
            $('.residentsRequests').append('<a class="dropdown-item" href="#"><div class="row"><div class="col-md-10">' +
                '<h5>' + req.resident.owner.user.login + '</h5>' +
                '<p class="lead">' + req.extraInfo + '</p></div><form method="post" action="/housingresidentrequests/residentId=' + req.residentId + '/houseId=' + houseId +'/apply" class="col-md-2"><button class="btn btn-outline-success">&#9989;</button></form></div>' +
                '</a>');
        }
    });
}


function loadHousePages() {
    $.get('/housing/houses/count').done(function (data) {
        let count = data;
        let pagesCount = getNumberOfPages(count);
        for (let i = 1; i <= pagesCount; i++) {
            $('.pagination').append('<li class="page-item"><a class="page-link" href="/Housing/Houses?page=' + i + '">' + i + '</a></li>');
        }
    });
}

function getNumberOfPages(count) {
    let res = 0;
    for (let i = 0; i <= count; i++) {
        if (i % 4 == 0) res++;
        if (count - i >= 4) {
            res++;
            break;
        }
    }
    return res;
}