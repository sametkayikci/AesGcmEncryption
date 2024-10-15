namespace AesGcmEncryption;

/// <summary>
/// Interface for AES-GCM encryption and decryption operations.
/// </summary>
public interface IAesGcmEncryptionService
{
    /// <summary>
    /// Encrypts the given plaintext using AES-GCM NoPadding.
    /// </summary>
    /// <param name="plainText">The plaintext to encrypt.</param>
    /// <param name="key">The encryption key (256-bit).</param>
    /// <param name="nonce">The nonce (12 bytes recommended for AES-GCM).</param>
    /// <param name="associatedData">Optional associated data for additional authentication.</param>
    /// <returns>The ciphertext combined with the authentication tag.</returns>
    byte[] Encrypt(byte[] plainText, byte[] key, byte[] nonce, byte[] associatedData);

    /// <summary>
    /// Decrypts the given ciphertext using AES-GCM NoPadding.
    /// </summary>
    /// <param name="cipherText">The ciphertext to decrypt, which includes the tag.</param>
    /// <param name="key">The encryption key (256-bit).</param>
    /// <param name="nonce">The nonce (same as used during encryption).</param>
    /// <param name="associatedData">Optional associated data for additional authentication.</param>
    /// <returns>The decrypted plaintext.</returns>
    byte[] Decrypt(byte[] cipherText, byte[] key, byte[] nonce, byte[] associatedData);
}