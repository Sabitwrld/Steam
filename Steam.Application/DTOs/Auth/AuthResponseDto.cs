namespace Steam.Application.DTOs.Auth
{
    /// <summary>
    /// Uğurlu autentikasiya (login və ya register) sonrası frontend-ə göndərilən cavab.
    /// JWT tokeni və iç-içə `User` obyekti saxlayır.
    /// </summary>
    public record AuthResponseDto
    {
        public string Token { get; init; } = null!;
        public DateTime Expiration { get; init; }

        /// <summary>
        /// Frontend-in gözlədiyi istifadəçi məlumatlarını saxlayan obyekt.
        /// </summary>
        public UserLoginResponseDto User { get; init; } = null!;
    }
}