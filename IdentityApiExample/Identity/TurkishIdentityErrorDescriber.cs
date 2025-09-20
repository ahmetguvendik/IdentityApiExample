using Microsoft.AspNetCore.Identity;

namespace IdentityApiExample.Identity;

public class TurkishIdentityErrorDescriber : IdentityErrorDescriber
{
    public override IdentityError DefaultError() => new()
    {
        Code = nameof(DefaultError),
        Description = "Bilinmeyen bir hata oluştu."
    };

    public override IdentityError ConcurrencyFailure() => new()
    {
        Code = nameof(ConcurrencyFailure),
        Description = "Eşzamanlılık hatası oluştu. Lütfen tekrar deneyin."
    };

    public override IdentityError PasswordMismatch() => new()
    {
        Code = nameof(PasswordMismatch),
        Description = "Geçersiz parola."
    };

    public override IdentityError InvalidToken() => new()
    {
        Code = nameof(InvalidToken),
        Description = "Geçersiz veya süresi dolmuş doğrulama anahtarı."
    };

    public override IdentityError LoginAlreadyAssociated() => new()
    {
        Code = nameof(LoginAlreadyAssociated),
        Description = "Bu kullanıcı için oturum zaten ilişkilendirilmiş."
    };

    public override IdentityError InvalidUserName(string userName) => new()
    {
        Code = nameof(InvalidUserName),
        Description = $"Geçersiz kullanıcı adı: {userName}."
    };

    public override IdentityError InvalidEmail(string email) => new()
    {
        Code = nameof(InvalidEmail),
        Description = $"Geçersiz e‑posta adresi: {email}."
    };

    public override IdentityError DuplicateUserName(string userName) => new()
    {
        Code = nameof(DuplicateUserName),
        Description = $"'{userName}' kullanıcı adı zaten alınmış."
    };

    public override IdentityError DuplicateEmail(string email) => new()
    {
        Code = nameof(DuplicateEmail),
        Description = $"'{email}' e‑posta adresi zaten kayıtlı."
    };

    public override IdentityError PasswordTooShort(int length) => new()
    {
        Code = nameof(PasswordTooShort),
        Description = $"Parola en az {length} karakter olmalıdır."
    };

    public override IdentityError PasswordRequiresNonAlphanumeric() => new()
    {
        Code = nameof(PasswordRequiresNonAlphanumeric),
        Description = "Parola en az bir özel karakter içermelidir."
    };

    public override IdentityError PasswordRequiresDigit() => new()
    {
        Code = nameof(PasswordRequiresDigit),
        Description = "Parola en az bir rakam içermelidir."
    };

    public override IdentityError PasswordRequiresLower() => new()
    {
        Code = nameof(PasswordRequiresLower),
        Description = "Parola en az bir küçük harf içermelidir."
    };

    public override IdentityError PasswordRequiresUpper() => new()
    {
        Code = nameof(PasswordRequiresUpper),
        Description = "Parola en az bir büyük harf içermelidir."
    };

    public override IdentityError PasswordRequiresUniqueChars(int uniqueChars) => new()
    {
        Code = nameof(PasswordRequiresUniqueChars),
        Description = $"Parola en az {uniqueChars} farklı karakter içermelidir."
    };
}


