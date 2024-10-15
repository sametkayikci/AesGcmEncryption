namespace AesGcmEncryption.Tests;

public class AesGcmEncryptionServiceTests
{
    private readonly AesGcmEncryptionService _encryptionService = new();

    [Fact]
    public void Given_ValidPlainText_When_EncryptIsCalled_Then_ShouldReturnCipherText()
    {
        // Arrange
        var plainText = "Hello World!"u8.ToArray();
        var key = EncryptionHelper.GenerateRandomBytes(32); // 256-bit key
        var nonce = EncryptionHelper.GenerateRandomBytes(12); // Recommended 12 bytes
        var associatedData = "AdditionalData"u8.ToArray();

        // Act
        var cipherText = _encryptionService.Encrypt(plainText, key, nonce, associatedData);

        // Assert
        Assert.NotNull(cipherText);
        Assert.NotEmpty(cipherText);
        Assert.True(cipherText.Length > plainText.Length);
    }

    [Fact]
    public void Given_ValidCipherText_When_DecryptIsCalled_Then_ShouldReturnPlainText()
    {
        // Arrange
        var plainText = "Hello World!"u8.ToArray();
        var key = EncryptionHelper.GenerateRandomBytes(32); // 256-bit key
        var nonce = EncryptionHelper.GenerateRandomBytes(12); // Recommended 12 bytes
        var associatedData = "AdditionalData"u8.ToArray();

        // Act
        var cipherText = _encryptionService.Encrypt(plainText, key, nonce, associatedData);
        var decryptedText = _encryptionService.Decrypt(cipherText, key, nonce, associatedData);

        // Assert
        Assert.NotNull(decryptedText);
        Assert.Equal(plainText, decryptedText);
    }

    [Fact]
    public void Given_InvalidKey_When_DecryptIsCalled_Then_ShouldThrowAuthenticationTagMismatchException()
    {
        // Arrange
        var plainText = "Hello World!"u8.ToArray();
        var key = EncryptionHelper.GenerateRandomBytes(32);
        var wrongKey = EncryptionHelper.GenerateRandomBytes(32);
        var nonce = EncryptionHelper.GenerateRandomBytes(12);
        var associatedData = "AdditionalData"u8.ToArray();

        var cipherText = _encryptionService.Encrypt(plainText, key, nonce, associatedData);

        // Act & Assert
        Assert.Throws<AuthenticationTagMismatchException>(() =>
            _encryptionService.Decrypt(cipherText, wrongKey, nonce, associatedData));
    }
}