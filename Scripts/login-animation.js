// login-animation.js

// Sayfanın HTML içeriği tamamen yüklendiğinde çalıştır
document.addEventListener('DOMContentLoaded', function () {

    // Gerekli HTML elementlerini ID'lerine göre seç
    const ogretmenGirisGecisButton = document.getElementById('ogretmenGirisiGecis');
    const ogrenciGirisGecisButton = document.getElementById('ogrenciGirisiGecis');
    const container = document.getElementById('container');

    // Gizli alanı (HiddenField) class adına göre seç
    const panelStateHiddenField = document.getElementById(window.hfPanelStateClientID || 'hfPanelState');

    // Seçilen elementlerin var olup olmadığını kontrol et (hata ayıklama için önemli)
    if (ogretmenGirisGecisButton && ogrenciGirisGecisButton && container && panelStateHiddenField) {

        ogretmenGirisGecisButton.addEventListener('click', () => {
            container.classList.add("right-panel-active");
            panelStateHiddenField.value = 'sign-up';
        });

        ogrenciGirisGecisButton.addEventListener('click', () => {
            container.classList.remove("right-panel-active");
            panelStateHiddenField.value = 'sign-in';
        });

        // Sayfa yüklendiğinde durumu kontrol et
        if (panelStateHiddenField.value === 'sign-up') {
             container.classList.add("right-panel-active");
        } else {
             container.classList.remove("right-panel-active");
        }

    } else {
        // Elementlerden biri bulunamazsa konsola detaylı hata yaz
        console.error("Login animasyonu için gerekli HTML elementlerinden biri veya birkaçı bulunamadı!");
        if (!ogretmenGirisGecisButton) console.error("ID'si 'ogretmenGirisiGecis' olan buton bulunamadı.");
        if (!ogrenciGirisGecisButton) console.error("ID'si 'ogrenciGirisiGecis' olan buton bulunamadı.");
        if (!container) console.error("ID'si 'container' olan div bulunamadı.");
        if (!panelStateHiddenField) console.error("CSS Class'ı 'panel-state-hidden-field' olan gizli alan (HiddenField) bulunamadı.");
        if (!panelStateHiddenField) console.error("HiddenField ('" + (window.hfPanelStateClientID || 'hfPanelState') + "') bulunamadı. ClientID script'i doğru çalışıyor mu?");
    }

}); // DOMContentLoaded sonu