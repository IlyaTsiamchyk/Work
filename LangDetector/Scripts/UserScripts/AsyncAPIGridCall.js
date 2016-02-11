//Увеличивает количество запросов для пользователя на 1 и 
function increaseAmountOfQueries(id) {
    var baseUri = "/api/TopRequestsApi";

    $.ajax({
        url: baseUri,
        type: "PUT",
        data: JSON.stringify(id),
        dataType: "json",
        contentType: "application/json",

    });
}

//Отправляет запрос на сервет для получения массива значений язык-процент соответствия.
function getLanguageGrid(id, text, selectorForGrid) {
    var baseUri = "/api/LanguageGridApi?id=" + id;
    /*var dataToPost = $('#text').val();
    $.getJSON(url, function(data) {
        $("#get").text(data);
    })*/
    $.ajax({
        url: baseUri,
        type: "POST",
        data: JSON.stringify(text),
        dataType: "json",
        contentType: "application/json",

        statusCode: {
            500: function (data) {
                //fillPopup(data, selectorForGrid); 
                $(selectorForGrid).html("<p>Server error.</p>");
            }
        },

        success: function(data) { fillPopup(data, selectorForGrid); }
    });
}

//Fill the bootstrap table by data value.
function fillPopup(data, selectorForGrid) {
    var str = "";
    //Создание таблицы для bootstrap.
    for (var i = 0, dataLn = data.length; i < dataLn; i++) {
        str = str + "<div class='row'>";
        str = str + "<div class = 'col-md-4'><p>"
                + data[i].Language + "</p></div>";
        str = str + "<div class = 'col-md-4'><p>"
                + data[i].ChanceOfLanguage + "%</p></div>";
        str = str + "</div>";
    }
    $(selectorForGrid).html(str);
}

// Создание тултипа.
function Tooltip() {
    this.tooltip = document.createElement("div");
    this.tooltip.style.position = "absolute";
    this.tooltip.style.visibility = "hidden";
    this.tooltip.className = "mytooltip";
}

// Установка содержимого для подсказки и отображение.
Tooltip.prototype.show = function (text, x, y) {
    this.tooltip.innerHTML = text;
    this.tooltip.style.left = x + "px";
    this.tooltip.style.top = y + "px";
    this.tooltip.style.visibility = "visible";

    // Добавление подсказки в документ, если она еще не присутствует.
    if (this.tooltip.parentNode != document.body)
        document.body.appendChild(this.tooltip);
};

Tooltip.prototype.hide = function () {
    this.tooltip.style.visibility = "hidden";
};

var t = new Tooltip();

function mouseMoveHandler(e) {
    if (!e) e = window.event;
    t.show("Нажмите на любое слово, чтобы получить его язык. Мы знаем русский, английский, португальский, испанский и болгарский языки.", e.clientX + 10, e.clientY + 10);
}

function mouseOutHandler() {
    t.hide();
}

// Найти слово, на которое нажал пользователь.
function getSelectedWordByClick() {
    //TODO: Доработать для старых браузеров.
    //Для очень старых браузеров (доработать).
    if (document.selection) {
        return document.selection.createRange()/*.parentElement()*/;
    } else {
        // Получить выбранный элемент.
        var selection = window.getSelection();
        if (selection.rangeCount > 0) {
            // Получить смещение для буквы, которая была нажата.
            var startIndex = selection.focusOffset;

            //Получить текстовое содержимое элемента.
            var text = selection.getRangeAt(0).startContainer.textContent;

            var i = 1;
            var res = text.charAt(startIndex);
            var char = "";

            //Если пользователь нажал пробельный символ, то return.
            if (res === " " || res === "" || res === "\n") {
                return res;
            }

            //Перебираем символы слева и справа от выбранного пользователем до пробельного символа. Формируем выбранное слово.
            while (i <= startIndex) {
                char = text.charAt(startIndex - i);
                if (/\s/.test(char))
                    break;
                res = char + res;
                i++;
            }
            i = 1;

            while (true) {
                char = text.charAt(startIndex + i);
                if (/\s/.test(char) || char === "")
                    break;
                res = res + char;
                i++;
            }
            return res;
            /*return selection.getRangeAt(0).startContainer.parentNode*/;
        }
    }
}