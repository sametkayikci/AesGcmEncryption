namespace AesGcmEncryption;

/// <summary>
/// Service for performing AES-GCM encryption and decryption with NoPadding.
/// </summary>
public class AesGcmEncryptionService : IAesGcmEncryptionService
{
    /// <summary>
    /// Encrypts the given plaintext using AES-GCM with the provided key, nonce, and optional associated data.
    /// </summary>
    /// <param name="plainText">The plaintext to encrypt as a byte array.</param>
    /// <param name="key">The encryption key as a byte array (256-bit).</param>
    /// <param name="nonce">The nonce (initialization vector) as a byte array (12 bytes recommended).</param>
    /// <param name="associatedData">Optional associated data for additional authentication. It is not encrypted but is used for integrity checks.</param>
    /// <returns>The encrypted ciphertext with the authentication tag appended.</returns>
    public byte[] Encrypt(byte[] plainText, byte[] key, byte[] nonce, byte[] associatedData)
    {
        var cipherText = new byte[plainText.Length];
        var tag = new byte[16]; // GCM tag length is always 16 bytes

        using var aes = new AesGcm(key);
        aes.Encrypt(nonce, plainText, cipherText, tag, associatedData);

        // Combine cipherText and tag
        var result = new byte[cipherText.Length + tag.Length];
        cipherText.CopyTo(result, 0);
        tag.CopyTo(result, cipherText.Length);

        return result;
    }

    /// <summary>
    /// Decrypts the given ciphertext using AES-GCM with the provided key, nonce, and optional associated data.
    /// </summary>
    /// <param name="cipherText">The ciphertext to decrypt, which includes the tag.</param>
    /// <param name="key">The encryption key as a byte array (256-bit).</param>
    /// <param name="nonce">The nonce (initialization vector) as a byte array (must match the one used during encryption).</param>
    /// <param name="associatedData">Optional associated data used for authentication (must match the one used during encryption).</param>
    /// <returns>The decrypted plaintext as a byte array.</returns>
    public byte[] Decrypt(byte[] cipherText, byte[] key, byte[] nonce, byte[] associatedData)
    {
        var tag = new byte[16]; // GCM tag length is 16 bytes
        var actualCipherText = new byte[cipherText.Length - tag.Length];
        Array.Copy(cipherText, actualCipherText, actualCipherText.Length);
        Array.Copy(cipherText, actualCipherText.Length, tag, 0, tag.Length);

        var plainText = new byte[actualCipherText.Length];

        using var aes = new AesGcm(key);
        aes.Decrypt(nonce, actualCipherText, tag, plainText, associatedData);

        return plainText;
    }
}