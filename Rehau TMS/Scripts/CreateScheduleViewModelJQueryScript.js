$(document).ready(function () {
    $("#WorkType").ready(function () {
        $("#ArticleId").attr('disabled', 'disabled');
        $.get("/ScheduleViewModels/GetArticlesList", { WorkTypeModelsId: $("#WorkType").val() }, function (data) {
            $("#ToolsModelsId").empty();
            $("#OptionsModelsId").empty();
            $('#OptionsAdditionalModelsId').empty;

            $("#ToolsModelsId").attr('disabled', 'disabled');
            $('#OptionsModelsId').attr('disabled', 'disabled');
            $('#OptionsAdditionalModelsId').attr('disabled', 'disabled');

            if ($("#ArticleId").empty() && $("#WorkType").val() != 1) {
                $("#ArticleId").attr('disabled', 'disabled');
            }
            else
                $("#ArticleId").removeAttr('disabled');

            $.each(data, function (index, row) {
                $("#ArticleId").append("<option value='" + row.Id + "'>" + row.Name + "</option>")
            });

            $(function () {
                $("#ArticlesModelsId option[value='0']").attr("disabled", "disabled");
                $("#ToolsModelsId option[value='0']").attr("disabled", "disabled");
            });
        });
    })

    $("#WorkType").change(function () {
        $.get("/ScheduleViewModels/GetArticlesList", { WorkTypeModelsId: $("#WorkType").val() }, function (data) {
            $("#ArticleId").empty()
            $("#ToolsModelsId").empty();
            $("#OptionsModelsId").empty();
            $("#OptionsAdditionalModelsId").empty();


            if ($("#ArticleId").empty() && $("#WorkType").val() != 1) {
                $("#ArticleId").attr('disabled', 'disabled');
                $("#ToolsModelsId").attr('disabled', 'disabled');
                $('#OptionsModelsId').attr('disabled', 'disabled');
                $('#OptionsAdditionalModelsId').attr('disabled', 'disabled');

            }
            else {
                $("#ArticleId").removeAttr('disabled');
                $("#ToolsModelsId").attr('disabled', 'disabled');
                $('#OptionsModelsId').attr('disabled', 'disabled');
                $('#OptionsAdditionalModelsId').attr('disabled', 'disabled');

                $.each(data, function (index, row) {
                    $("#ArticleId").append("<option value='" + row.Id + "'>" + row.Name + "</option>")
                });
            }

            $(function () {
                $("#ArticlesModelsId option[value='0']").attr("disabled", "disabled");
            });
        });
    })

    $("#ArticleId").change(function () {
        $.get("/ScheduleViewModels/GetToolsList", { ArticlesModelsId: $("#ArticleId").val() }, function (data) {
            $("#OptionsModelsId").empty();
            $("#OptionsAdditionalModelsId").empty();

            $("#ToolsModelsId").removeAttr('disabled');

            if ($("#ToolsModelsId").empty() && $("#ArticleId") == 0) {
                $("#ToolsModelsId").attr('disabled', 'disabled');
            }
            else {
                $("#ToolsModelsId").removeAttr('disabled');
            }

            $('#OptionsModelsId').attr('disabled', 'disabled');
            $('#OptionsAdditionalModelsId').attr('disabled', 'disabled');

            $.each(data, function (index, row) {
                $("#ToolsModelsId").append("<option value='" + row.Id + "'>" + row.Name + "</option>")
            });

            $(function () {
                $("#ToolsModelsId option[value='0']").attr("disabled", "disabled");
            });
        });
    })

    $("#ToolsModelsId").change(function () {
        $.get("/ScheduleViewModels/GetOptionsList", { ToolsModelsId: $("#ToolsModelsId").val() }, function (data) {
            $("#OptionsModelsId").empty();
            $("#OptionsAdditionalModelsId").empty();

            $("#OptionsModelsId").removeAttr('disabled');
            $("#OptionsAdditionalModelsId").attr('disabled', 'disabled');

            $.each(data, function (index, row) {
                $("#OptionsModelsId").append("<option value='" + row.Id + "'>" + row.Name + "</option>")
            });

            $(function () {
                $("#OptionsModelsId option[value='0']").attr("disabled", "disabled");
            });
        });
    })

    $("#OptionsModelsId").change(function () {
        $.get("/ScheduleViewModels/GetOptionsAdditionalList", { OptionsModelsId: $("#OptionsModelsId").val() }, function (data) {
            $("#OptionsAdditionalModelsId").empty();

            $("#OptionsAdditionalModelsId").removeAttr('disabled');

            if ($("#OptionsAdditionalModelsId").empty() && $("#OptionsModelsId").val() != 1) {
                $("#OptionsAdditionalModelsId").attr('disabled', 'disabled');
            }
            else {
                $("#ToolsModelsId").removeAttr('disabled');


                $.each(data, function (index, row) {
                    $("#OptionsAdditionalModelsId").append("<option value='" + row.Id + "'>" + row.Name + "</option>")
                });
            }

            $(function () {
                $("#OptionsAdditionalModelsId option[value='0']").attr("disabled", "disabled");
            });
        });
    })
});