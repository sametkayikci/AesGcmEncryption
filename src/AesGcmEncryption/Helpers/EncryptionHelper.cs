namespace AesGcmEncryption.Helpers;

public static class EncryptionHelper
{
    /// <summary>
    /// Generates a byte array of random bytes of the specified length.
    /// </summary>
    public static byte[] GenerateRandomBytes(int length)
    {
        var randomBytes = new byte[length];
        Random.Shared.NextBytes(randomBytes);
        return randomBytes;
    }
}