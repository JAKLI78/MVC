function NewUser() {
    var url = "/Users/NewUser";
    window.location = url;
}

function DeleteUser(id) {
    if (confirm("Вы точно хотите удалиь этого пользователя?")) {
        $.ajax({
            type: "DELETE",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            url: "../Users/DeleteUser?id=" + id,
            data: "{ }",
            success: function(response) {
                window.location = response.url;
            }
        });
    }
}