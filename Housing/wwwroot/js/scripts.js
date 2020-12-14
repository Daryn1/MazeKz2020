$(document).ready(function () {
    $('#addHouseForm').submit(function (e) {
        e.preventDefault();
        let house = {
            Name: $('#houseName').val(),
            Street: $('#houseStreet').val(),
            Type: $('#houseType').val(),
            Price: $('#housePrice').val(),
            Info: $('#houseInfo').val()
        };
        $.ajax({
            type: "POST",
            url: "/houses/add",
            data: JSON.stringify(house),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                location.href = "/housing/houses";
            },
            error: function (response) {
                $('#houseError').html(response.responseText);
            }
        });
    });
});