function selectedCultureChanged() {
  var selectedCultureCode = document.getElementById("cultureSelector").value;

  setCookie(".AspNetCore.Culture", "c=" + selectedCultureCode + "|uic=" + selectedCultureCode);
  location.reload();
}

function setCookie(name, value, days) {
  var expires = "";

  if (days) {
    var date = new Date();

    date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
    expires = "; expires=" + date.toUTCString();
  }

  document.cookie = name + "=" + (value || "") + expires + "; path=/";
}