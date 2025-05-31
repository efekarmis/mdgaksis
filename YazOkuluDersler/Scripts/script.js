// ALERTLARIN 5 SANİYEDE KAPANMASINI SAĞLAR 
// Sayfa yüklendiğinde çalışır
    window.onload = function () {
        // Tüm alert'leri seç
        const alerts = document.querySelectorAll('.alert');

        // Her bir alert için süre ayarla
        alerts.forEach(alert => {
            setTimeout(() => {
                alert.style.transition = "opacity 0.5s ease";
                alert.style.opacity = "0"; // Görünmez yap
                setTimeout(() => alert.remove(), 500); // DOM'dan kaldır
            }, 5000); // 5 saniye sonra kaybolur
        });
    };