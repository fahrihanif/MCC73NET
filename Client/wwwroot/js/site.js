//let judul = document.getElementById("judul");


//let paragraf = document.getElementsByTagName("p")[0];
//paragraf.style.backgroundColor = "red";

//let variable1 = document.getElementsByClassName("list");

//judul.onclick = function () {
//    paragraf.innerHTML = "berubah!";
//}

//let ul = document.getElementsByTagName("ul");
//let li = ul.getElementsByTagName("li");

//judul.onclick = () => {
//    judul.style.backgroundColor = "cyan";
//    judul.innerHTML = "saya rubah dari JS";
//}

//judul.addEventListener("click", function () {
//    paragraf.innerHTML = "berubah!";
//});

//judul.addEventListener("click", function () {
//    judul.style.backgroundColor = "cyan";
//    judul.innerHTML = "saya rubah dari JS";
//});

//paragraf.addEventListener("mouseenter", function () {
//    paragraf.innerHTML = "Mashokkkk!!!";
//});

//paragraf.addEventListener("mouseleave", function () {
//    paragraf.innerHTML = "KELOARRR!!!";
//});

//let list3 = document.querySelector("li.list#list:nth-child(3)");

//function berubah() {
//    for (var i = 0; i < variable1["length"]; i++) {
//        variable1[i].innerHTML = "diubah dari onclick html";
//    }
//}

////Array
//let array = [1, 2, 3, 4];
////insert last
//array.push("halo");
//console.log(array);
////delete last
//array.pop();
//console.log(array);
////insert first
//array.unshift("test");
//console.log(array);
////delete first
//array.shift();
//console.log(array);

////array multi dimensi
//let arrayMulti = [1, 2, 3, ['a', 'b', 'c'], true];
//console.log(arrayMulti);

//let tambah = (x, y) => { return x + y; }
//console.log(tambah(5, 10));

////array associative
//array["asda"]

//////constanta
////const nilai = 50;
////console.log(nilai);
////nilai = 90;
////console.log(nilai);


////object
//const mhs = {
//    nama: "budi",
//    nim: "a111293192",
//    gender: "laki",
//    hobby: ["mancing", "tawuran", "ngegame"],
//    isActive: true
//};

//console.log(mhs);
//const user = {};
//user.username = "budilagi";
//user.password = "inipasswordbudi";
//console.log(user);

//user.password = "berubah";
//console.log(user);

////array of object
//const animals = [
//    { name: "budi", species: "dog", class: {name:"mamalia"}},
//    { name: "tono", species: "dog", class: {name:"mamalia"}},
//    { name: "nemo", species: "fish", class: {name:"invertebrata"}},
//    { name: "dory", species: "fish", class: {name:"invertebrata"}},
//    { name: "james", species: "dog", class: {name:"mamalia"}}
//]

//console.log(animals);
////function onlydog, yaitu looping ke animals=> yg diambil hanya species dog
////const onlyDog
////jika species = fish => maka ubah class name menjadi 'non-mamalia'

///*const onlyDog = [];*/
////for (var i = 0; i < animals.length; i++) {
////    if (animals[i].species == "dog") {
////        onlyDog.push(animals[i]);
////    }
////}

///*onlyDog.push(animals.filter(animal => animal.species == "dog"));*/
////console.log(onlyDog);

//let detailAnimal;
//detailAnimal = animals.map(animal => {
//    return {
//        name: animal.name,
//        species: animal.species,
//        isFish: animal.species == "fish" ? true:false
//    }
//})

//console.log(detailAnimal);

//let data = {
//    series: [30,20],
//    labels: ["cowok","cewek"]
//}

//jquery

//$("#judul").click(function () {
//    $("#judul").css("background-color", "red");
//    $("#judul").html("Berubah!!")
//})

//AJAX => Asynchronous Javascript and XML
$.ajax({
    url: "https://pokeapi.co/api/v2/pokemon/",
    //success: function (result) {
    //    console.log(result);
    //}
}).done((result) => {
    //console.log(result.results);
    let temp = ""
    //for (var i = 0; i < result.results.length; i++) {
    //    temp += "<li>" + result.results[i].name +"</li>";
    //}
    //$("#listPoke").html(temp);

    $.each(result.results, function (key, val) {
        //old way
        //temp += "<tr>\
        //            <td>"+(key+1)+"</td>\
        //            <td>"+val.name+"</td>\
        //            <td>"+val.name+"</td>\
        //        </tr>"
        //jalan ninja ku / pakai backtick / literal template
        temp += `<tr>
                    <td>${key+1}</td>
                    <td>${val.name}</td>
                    <td><button class="btn btn-primary" onclick="detailPoke('${val.url}')" data-bs-toggle="modal" data-bs-target="#modalPokeDetail">Detail</button></td>
                </tr>`
    })
    $("#tablePoke").html(temp);


}).fail((err) => {
    console.log(err);
})


function detailPoke(stringUrl) {
    $.ajax({
        url: stringUrl,
        success: function (result) {
            $(".modal-title").html(result.name);
        }
    })
}
