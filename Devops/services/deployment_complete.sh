#!/bin/bash

# Gönderilecek kişiler
RECIPIENTS=("desteci.mehmet02@gmail.com" "ataaymankuy@gmail.com" "efebaraan@gmail.com" "aliozturkinfo@gmail.com" "feyzaozcini@gmail.com" "birliknil@gmail.com")

LAST_COMMIT_HASH=$(git log -1 --format="%H" | cut -c1-7)
LAST_COMMIT_AUTHOR=$(git log -1 --format="%an")
LAST_COMMIT_DATE=$(git log -1 --format="%cd")
LAST_COMMIT_MSG=$(git log -1 --format="%s")

SUBJECT="[Deployment Tamamlandı]✅ - Pusula.Training.HealthCare.Team1 - $LAST_COMMIT_HASH"


BODY="Merhaba,\n\nDeployment işlemi başarıyla tamamlanmıştır.\n\nSon Commit Detayları:\n- Commit ID: $LAST_COMMIT_HASH\n- Developer: $LAST_COMMIT_AUTHOR\n- Tarih: $LAST_COMMIT_DATE\n- Mesaj: $LAST_COMMIT_MSG\n\nLütfen uygulamanın doğru çalıştığından emin olmak için gerekli kontrolleri yapmayı unutmayın.\n\nTakımınızın DevOps sorumlusu olarak, iyi çalışmalar dilerim, ellerinize sağlık!😊"

for RECIPIENT in "${RECIPIENTS[@]}"; do
    echo -e "To: $RECIPIENT\nSubject: $SUBJECT\n\n$BODY" | ssmtp "$RECIPIENT"
    if [ $? -eq 0 ]; then
        echo "E-posta başarıyla gönderildi: $RECIPIENT"
    else
        echo "E-posta gönderimi başarısız oldu: $RECIPIENT"
    fi
done
