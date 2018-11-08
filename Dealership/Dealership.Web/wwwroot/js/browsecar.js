$(function () {
    $(".brand-scriptSelect").change(function () {
        var selectedBrandId = $(this).val();
        var url = "/Car/GetModelsByBrandId?brandId=" + selectedBrandId;
        $.get({
            url: url
        }).done(function (data) {
            var modelDropDown = $(".model-scriptSelect");
            modelDropDown.empty();
            var defaultOption = $("<option />").val("-1").text("All");
            modelDropDown.append(defaultOption);
            data.forEach(function (currentElement) {
                var modelOption = $("<option />").val(currentElement.id).text(currentElement.name);
                modelDropDown.append(modelOption);
            });
        });
    });
});