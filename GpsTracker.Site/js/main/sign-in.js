// var temp = {
//     user: USER,
//     person: PERSON
// }
// var USER;
// var PERSON;

// $(window).bind("load", function () {
//     alert(JSON.stringify(temp));
// });
// $("#sign_button").bind("click", function send() {
//     var user = {
//         Login: $("#login").val(),
//         Password: $("#pass").val(),
//     }
//     var jsonResponse;
//     var userId;
  
//     var deviceId;
//     var jsonResponse2;
//     var jsonResponse3;

//     // 1. Создаём новый объект XMLHttpRequest
//     var xhr = new XMLHttpRequest();

//     // 2. Конфигурируем его: GET-запрос на URL 'phones.json'
//     xhr.open('POST', 'api/web/user/login', false);
//     xhr.setRequestHeader("Content-Type", "application/json");
//     // 3. Отсылаем запрос
//     xhr.send(JSON.stringify(user));

//     // 4. Если код ответа сервера не 200, то это ошибка
//     if (xhr.status != 200) {
//         // обработать ошибку
//         alert(xhr.status + ': ' + xhr.statusText); // пример вывода: 404: Not Found
//     } else {
//         // вывести результат
//         alert(xhr.responseText); // responseText -- текст ответа.
//         jsonResponse = JSON.parse(xhr.responseText);
//         userId = jsonResponse["UserId"];
//         deviceId = jsonResponse["DeviceId"];
//         USER = jsonResponse;
//     }

//     var person = {
//         userId: userId
//     }

//     var xhr2 = new XMLHttpRequest();

//     xhr2.open('POST', 'api/web/person', false);
//     xhr2.setRequestHeader("Content-Type", "application/json");
//     xhr2.send(JSON.stringify(person));
//     if (xhr2.status != 200) {
//         // обработать ошибку
//         alert(xhr2.status + ': ' + xhr2.statusText); // пример вывода: 404: Not Found
//     } else {
//         // вывести результат
//         alert(xhr2.responseText); // responseText -- текст ответа.
//         jsonResponse2 = JSON.parse(xhr2.responseText);
//         PERSON = jsonResponse2;
//     }

//     var xhr3 = new XMLHttpRequest();

//     xhr3.open('GET', 'index.html', false);
//     xhr3.setRequestHeader("Content-Type", "application/json");
//     xhr3.send(JSON.stringify(temp));
//     if (xhr3.status != 200) {
//         // обработать ошибку
//         alert(xhr3.status + ': ' + xhr3.statusText); // пример вывода: 404: Not Found
//     } else {
//         // вывести результат
//         //alert(xhr3.responseText); // responseText -- текст ответа.
        
//     }
//     //console.log(JSON.stringify(USER));
//     //console.log(JSON.stringify(PERSON));
//     //alert(USER);
//     //alert(PERSON);
// });