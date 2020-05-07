var categ = 0;
var min_rating = 0;
var sort = 0;
var min_price = 0;


function Load() {
    $('#select_min_price').val(min_price);
    $('#select_max_price').val(max_price);
    $('#select_min_rating').val(min_rating);
    $('#select_max_rating').val(max_rating);
}

$(document).ready(function () {
    $('#select_min_price').on('focusout', function () {
        if (isNaN(this.value) || this.value < 0) { $('#select_min_price').val(min_price); }
        else {
            min_price = this.value;
            setLocation();
            open();
        }
    });
    $('#select_max_price').on('focusout', function () {
        if (isNaN(this.value) || this.value < 0) { $('#select_max_price').val(max_price); }
        else {
            max_price = this.value;
            setLocation();
            open();
        }
    });
    $('#select_min_rating').on('focusout', function () {
        if (isNaN(this.value) || this.value < 0) { $('#select_min_rating').val(min_rating); }
        else {
            min_rating = this.value;
            setLocation();
            open();
        }
    });
    $('#select_max_rating').on('focusout', function () {
        if (isNaN(this.value) || this.value < 0) { $('#select_max_rating').val(max_rating); }
        else {
            max_rating = this.value;
            setLocation();
            open();
        }
    });
    $('#sort').on('change', function () {
        if (isNaN(this.value) || this.value < 0) { $('#sort').val(sort); }
        else {
            sort = this.value;
            setLocation();
            open();
        }
    });

    $(document).scroll(function () {
        $('#menu').css({
            top: $(document).scrollTop() + 74
        });
    });
});

function show(idx) {
    categ = idx;
    open();
}

function open() {
    $.ajax({
        url: "/time",
        data: ({ "category": categ, "min_price": min_price, "max_price": max_price, "min_rating": min_rating, "max_rating": max_rating, "sort": sort }),
        cache: false,
        success: function (html) {
            $("#content").html(html);
        }
    });
}

//Подмена в адресной строке
function setLocation() {
    var curLoc = '?category=' + categ + '&min_price=' + min_price + '&max_price=' + max_price + '&min_rating=' + min_rating + '&max_rating=' + max_rating + '&sort=' + sort;
    try {
        history.pushState(null, null, curLoc);
        return;
    } catch (e) { }
    location.hash = '#' + curLoc;
}

//Ищем в адресной строке параметр category
var url = new URL(window.location.href);
var c = url.searchParams.get("category");
var sort1 = url.searchParams.get("sort");
var min_price1 = url.searchParams.get("min_price");
var max_price1 = url.searchParams.get("max_price");
var min_rating1 = url.searchParams.get("min_rating");
var max_rating1 = url.searchParams.get("max_rating");

if (!isNaN(min_price1) && min_price1 != null && min_price1 >= 0) { min_price = min_price1; }
if (!isNaN(max_price1) && max_price1 != null && max_price1 >= 0) { max_price = max_price1; }
if (!isNaN(min_rating1) && min_rating1 != null && min_rating1 >= 0) { min_rating = min_rating1; }
if (!isNaN(max_rating1) && max_rating1 != null && max_rating1 >= 0) { max_rating = max_rating1; }
if (!isNaN(sort1) && sort1 != null && sort1 >= 0 && sort1 <= 4) { sort = sort1; }

show(c);  