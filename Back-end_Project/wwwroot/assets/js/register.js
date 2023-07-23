const loginForm = document.getElementById("login-form");
const registerForm = document.getElementById("register-form");

function showLoginForm() {
    loginForm.style.display = "block";
    registerForm.style.display = "none";
}

function showRegisterForm() {
    loginForm.style.display = "none";
    registerForm.style.display = "block";
}

loginForm.addEventListener("submit", function (event) {
    event.preventDefault();
    const username = document.getElementById("username").value;
    const password = document.getElementById("password").value;
    // Burada login işlemleri yapılabilir.
    console.log("Giriş yapıldı:", username);
});

registerForm.addEventListener("submit", function (event) {
    event.preventDefault();
    const username = document.getElementById("reg-username").value;
    const email = document.getElementById("email").value;
    const password = document.getElementById("reg-password").value;
    // Burada kayıt işlemleri yapılabilir.
    console.log("Kayıt olundu:", username, email);
});

showLoginForm(); // Sayfa yüklendiğinde varsayılan olarak login formunu göster

