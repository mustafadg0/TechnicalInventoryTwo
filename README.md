Teknik Envanter 2 - Event Tarama

Bu proje, asenkron bir şekilde rastgele öncelikli eventler üreten ve bu eventleri işleyerek belirli kurallar çerçevesinde uyarılar (alert) üreten bir sistemdir. 
Event Producer modülü belirli zaman aralıklarında event üretirken, Event Consumer modülü eventleri işler ve ardışık üç aynı öncelikli event tespit edildiğinde bir alert üretir.

Proje Kapsamı

Event Producer:
Rastgele öncelikli eventler (Düşük, Orta, Yüksek) üretir.
Üretilen eventleri tabloya ekler.
Her event ekleme işlemi 3 saniye sürer.

Event Consumer:
Eventleri sırasıyla okur ve işler.
5 saniyede bir event okuma işlemi gerçekleştirir.
Ardışık üç eventin önceliği aynı ise bir Alert üretir.

Hedef:
20 dakika içinde 400 event üretilmesi.
Tüm eventlerin işlenmesi ve kurallara uygun tüm alert'lerin üretilmesi.
