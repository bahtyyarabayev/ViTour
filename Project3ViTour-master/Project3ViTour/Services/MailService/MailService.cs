using MailKit.Security;
using MimeKit;
using MailKit.Net.Smtp;

namespace Project3ViTour.Services.MailService
{
    public class MailService : IMailService
    {
        private readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendReservationMailAsync(string toEmail, string nameSurname, string tourTitle, DateTime tourDate, int childCount, int youthCount, int adultCount, decimal totalPrice)
        {
            var mail = new MimeMessage();
            mail.From.Add(new MailboxAddress("ViTour", _configuration["MailSettings:From"]));
            mail.To.Add(new MailboxAddress(nameSurname, toEmail));
            mail.Subject = "Rezervasyonunuz Alındı - ViTour";

            var body = new BodyBuilder
            {
                HtmlBody = $@"
<!DOCTYPE html>
<html>
<body style='margin:0;padding:0;background:#f0f4f8;font-family:Arial,sans-serif;'>

  <table width='100%' cellpadding='0' cellspacing='0' style='background:#f0f4f8;padding:40px 0;'>
    <tr>
      <td align='center'>
        <table width='600' cellpadding='0' cellspacing='0' style='max-width:600px;width:100%;'>

          <tr>
            <td style='background:#0C447C;border-radius:16px 16px 0 0;padding:40px 40px 32px;text-align:center;'>
              <div style='background:#185FA5;display:inline-block;padding:12px 20px;border-radius:50px;margin-bottom:20px;'>
                <span style='color:#E6F1FB;font-size:13px;font-weight:bold;letter-spacing:0.1em;text-transform:uppercase;'>Rezervasyon Onayı</span>
              </div>
              <h1 style='color:#ffffff;margin:0 0 8px;font-size:28px;font-weight:700;'>Rezervasyonunuz Alındı!</h1>
              <p style='color:#85B7EB;margin:0;font-size:15px;'>Merhaba <strong style='color:#E6F1FB;'>{nameSurname}</strong>, sizi aramızda görmekten mutluluk duyuyoruz.</p>
            </td>
          </tr>

          <tr>
            <td style='background:#ffffff;padding:32px 40px;'>

              <table width='100%' cellpadding='0' cellspacing='0' style='background:#E6F1FB;border-radius:12px;padding:24px;margin-bottom:24px;'>
                <tr>
                  <td style='padding:0 0 16px;'>
                    <p style='margin:0;font-size:11px;font-weight:bold;color:#378ADD;text-transform:uppercase;letter-spacing:0.1em;'>Tur Bilgileri</p>
                  </td>
                </tr>
                <tr>
                  <td>
                    <table width='100%' cellpadding='0' cellspacing='0'>
                      <tr>
                        <td style='padding:10px 0;border-bottom:1px solid #B5D4F4;'>
                          <span style='color:#185FA5;font-size:13px;'>Tur Adı</span>
                        </td>
                        <td style='padding:10px 0;border-bottom:1px solid #B5D4F4;text-align:right;'>
                          <strong style='color:#042C53;font-size:14px;'>{tourTitle}</strong>
                        </td>
                      </tr>
                      <tr>
                        <td style='padding:10px 0;border-bottom:1px solid #B5D4F4;'>
                          <span style='color:#185FA5;font-size:13px;'>Tur Tarihi</span>
                        </td>
                        <td style='padding:10px 0;border-bottom:1px solid #B5D4F4;text-align:right;'>
                          <strong style='color:#042C53;font-size:14px;'>{tourDate:dd MMMM yyyy}</strong>
                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>
              </table>

              <table width='100%' cellpadding='0' cellspacing='0' style='margin-bottom:24px;'>
                <tr>
                  <td width='32%' style='padding-right:8px;'>
                    <table width='100%' cellpadding='0' cellspacing='0' style='background:#E6F1FB;border-radius:12px;text-align:center;padding:20px 16px;'>
                      <tr><td><p style='margin:0;font-size:28px;font-weight:700;color:#185FA5;'>{childCount}</p></td></tr>
                      <tr><td><p style='margin:4px 0 0;font-size:11px;color:#378ADD;text-transform:uppercase;letter-spacing:0.06em;'>Çocuk</p></td></tr>
                    </table>
                  </td>
                  <td width='32%' style='padding:0 4px;'>
                    <table width='100%' cellpadding='0' cellspacing='0' style='background:#E6F1FB;border-radius:12px;text-align:center;padding:20px 16px;'>
                      <tr><td><p style='margin:0;font-size:28px;font-weight:700;color:#185FA5;'>{youthCount}</p></td></tr>
                      <tr><td><p style='margin:4px 0 0;font-size:11px;color:#378ADD;text-transform:uppercase;letter-spacing:0.06em;'>Genç</p></td></tr>
                    </table>
                  </td>
                  <td width='32%' style='padding-left:8px;'>
                    <table width='100%' cellpadding='0' cellspacing='0' style='background:#E6F1FB;border-radius:12px;text-align:center;padding:20px 16px;'>
                      <tr><td><p style='margin:0;font-size:28px;font-weight:700;color:#185FA5;'>{adultCount}</p></td></tr>
                      <tr><td><p style='margin:4px 0 0;font-size:11px;color:#378ADD;text-transform:uppercase;letter-spacing:0.06em;'>Yetişkin</p></td></tr>
                    </table>
                  </td>
                </tr>
              </table>

              <table width='100%' cellpadding='0' cellspacing='0' style='background:#0C447C;border-radius:12px;padding:24px;margin-bottom:24px;'>
                <tr>
                  <td>
                    <p style='margin:0 0 4px;font-size:12px;color:#85B7EB;text-transform:uppercase;letter-spacing:0.1em;'>Toplam Tutar</p>
                    <p style='margin:0;font-size:36px;font-weight:700;color:#ffffff;'>${totalPrice:F2}</p>
                  </td>
                  <td style='text-align:right;vertical-align:middle;'>
                    <div style='background:#185FA5;display:inline-block;padding:10px 20px;border-radius:8px;'>
                      <span style='color:#E6F1FB;font-size:13px;font-weight:bold;'>Onay Bekleniyor</span>
                    </div>
                  </td>
                </tr>
              </table>

              <table width='100%' cellpadding='0' cellspacing='0' style='background:#f8fafc;border:1px solid #B5D4F4;border-radius:12px;padding:20px;'>
                <tr>
                  <td>
                    <p style='margin:0 0 14px;font-size:11px;font-weight:bold;color:#378ADD;text-transform:uppercase;letter-spacing:0.1em;'>Önemli Bilgiler</p>
                    <p style='margin:0 0 8px;font-size:13px;color:#185FA5;'>&#10003;&nbsp; Ödeme detayları için sizinle iletişime geçilecektir.</p>
                    <p style='margin:0 0 8px;font-size:13px;color:#185FA5;'>&#10003;&nbsp; İptal için 48 saat öncesinde bildirim yapınız.</p>
                    <p style='margin:0 0 8px;font-size:13px;color:#185FA5;'>&#10003;&nbsp; Sorularınız için 7/24 destek hattımız aktiftir.</p>
                    <p style='margin:0;font-size:13px;color:#185FA5;'>&#10003;&nbsp; E-posta: info@vitour.com</p>
                  </td>
                </tr>
              </table>

            </td>
          </tr>

          <tr>
            <td style='background:#0C447C;border-radius:0 0 16px 16px;padding:24px 40px;text-align:center;'>
              <p style='margin:0 0 8px;color:#85B7EB;font-size:13px;'>Bizi tercih ettiğiniz için teşekkür ederiz.</p>
              <p style='margin:0;color:#378ADD;font-size:12px;'>© 2026 ViTour. Tüm hakları saklıdır.</p>
            </td>
          </tr>

        </table>
      </td>
    </tr>
  </table>

</body>
</html>"
            };

            mail.Body = body.ToMessageBody();

            using var smtp = new SmtpClient();
            smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;
            await smtp.ConnectAsync(
                _configuration["MailSettings:Host"],
                int.Parse(_configuration["MailSettings:Port"]),
                SecureSocketOptions.StartTls
            );
            await smtp.AuthenticateAsync(
                _configuration["MailSettings:Username"],
                _configuration["MailSettings:Password"]
            );
            await smtp.SendAsync(mail);
            await smtp.DisconnectAsync(true);
        }
    }
}
