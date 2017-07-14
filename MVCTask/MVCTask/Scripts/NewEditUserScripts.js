function AddNewTitleEdit() {
    var count = $(".title_box").length;
    $("#SomeTitle").append('<tr class="title_box"><td></td> <td><input type="text" name="Title[' +
        count +
        ']" value="" /></td> <td>' +
        '<input type="button" value="Remove" id="SomeAdd" onclick="RemoveTitleInput(' +
        count +
        ');" /></td></tr>');
};

function RemoveTitleInput(id) {
    console.log(id);
    $(".title_box:eq(" + id + ")").remove();
};

$(function() {
    $(".date-picker").datepicker();
});