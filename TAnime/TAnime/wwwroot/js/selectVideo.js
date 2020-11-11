function getEp(index) {
    var arr = document.getElementsByClassName("video");
    var arrA = document.getElementsByClassName("check");
    for (let i = 0; i < arr.length; i++) {
        if (i == index-1) {
            arr[i].style.display = 'block';
            arrA[i].style.background = "#ff6a00";
        } else {
            arr[i].style.display = 'none';
            arrA[i].style.background = "#212529";
        }
    }
}
function defaultVideo (){
    document.getElementsByClassName("video")[0].style.display = 'block';
    document.getElementsByClassName("check")[0].style.background = '#ff6a00';
    var arr = document.getElementsByClassName("video");
    var arrA = document.getElementsByClassName("check");
    for (let i = 1; i < arr.length; i++) {

        arr[i].style.display = 'none';
        arrA[i].style.background = "#212529";
    }
}
defaultVideo();


let id = document.querySelector('#movie-id').value;
console.log(id);
document.querySelector('iframe').addEventListener("click", myView(id));
function myView(id){
    $.ajax({
        url: `/Movie/UpdateView/${id}`,
        method: 'GET',
        contentType: 'JSON',
        success: function (data) {
            console.log(data.updateMovie);
        }
    });
}
