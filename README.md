
# AES-GCM Encryption and Decryption in .NET 8
[![Build Status](https://github.com/kullaniciadiniz/repo-adi/actions/workflows/ci.yml/badge.svg)](https://github.com/kullaniciadiniz/repo-adi/actions)



## Overview

This project provides a simple implementation of AES-GCM encryption and decryption using .NET 8. AES-GCM (Galois/Counter Mode) offers both encryption and authentication, making it a secure choice for protecting sensitive data. This service uses AES-GCM **without padding** for precise control over the data size.

### Key Features:
- **AES-GCM Encryption**: Provides both confidentiality and data integrity.
- **NoPadding**: No additional padding is applied to the plaintext, ensuring exact data size control.
- **Authentication**: Ensures the integrity of the encrypted data via an authentication tag (16 bytes).
- **Integration-Ready**: Can be used in applications requiring strong encryption, such as messaging apps, financial systems, and secure storage.

## Table of Contents

1. [Overview](#overview)
2. [Installation](#installation)
3. [How It Works](#how-it-works)
    - [Encrypt Method](#encrypt-method)
    - [Decrypt Method](#decrypt-method)
4. [Usage Example](#usage-example)
    - [Generating Random Bytes](#generating-random-bytes)
5. [Unit Tests](#unit-tests)
6. [Project Structure](#project-structure)
7. [License](#license)

## Installation

To install and use this project, follow these steps:

1. Clone the repository:
    ```bash
    git clone https://github.com/sametkayikci/AesGcmEncryption.git
    ```

2. Navigate into the project directory:
    ```bash
    cd AesGcmEncryption
    ```

3. Build the solution:
    ```bash
    dotnet build
    ```

4. Run unit tests to ensure everything is set up correctly:
    ```bash
    dotnet test
    ```

## How It Works

AES-GCM is a widely-used encryption algorithm that supports authenticated encryption. The GCM (Galois/Counter Mode) provides data integrity, so if the data is tampered with during transmission, it will be detected during decryption.

### Encrypt Method:
```csharp
public byte[] Encrypt(byte[] plainText, byte[] key, byte[] nonce, byte[] associatedData)
```
- **plainText**: The data to encrypt.
- **key**: The encryption key (256-bit recommended).
- **nonce**: A 12-byte initialization vector (IV) used for encryption.
- **associatedData**: Optional data used for authentication (not encrypted).

Returns the encrypted data combined with the authentication tag.

### Decrypt Method:
```csharp
public byte[] Decrypt(byte[] cipherText, byte[] key, byte[] nonce, byte[] associatedData)
```
- **cipherText**: The encrypted data combined with the tag.
- **key**: The same key used for encryption.
- **nonce**: The same nonce used for encryption.
- **associatedData**: Optional data used for authentication (must match the one used during encryption).

Returns the decrypted plaintext, or throws an exception if the tag doesn’t match.

## Usage Example

```csharp
byte[] plainText = System.Text.Encoding.UTF8.GetBytes("Hello World!");
byte[] key = EncryptionHelper.GenerateRandomBytes(32); // 256-bit key
byte[] nonce = EncryptionHelper.GenerateRandomBytes(12); // 12-byte nonce
byte[] associatedData = System.Text.Encoding.UTF8.GetBytes("AdditionalData");

// Encrypt the data
byte[] cipherText = _encryptionService.Encrypt(plainText, key, nonce, associatedData);

// Decrypt the data
byte[] decryptedText = _encryptionService.Decrypt(cipherText, key, nonce, associatedData);
```

### Generating Random Bytes

To generate cryptographically secure random bytes, use the `EncryptionHelper` class:

```csharp
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
```

This method is **thread-safe** and ensures that secure random bytes are generated, suitable for use in encryption processes.

## Unit Tests

Unit tests are provided using xUnit, following BDD (Behavior-Driven Development) style. Tests include:
- Verifying successful encryption and decryption.
- Checking for exceptions when invalid keys are used.
- Ensuring that tampering with the ciphertext or associated data causes decryption failure.

To run the unit tests:
```bash
dotnet test
```

## Project Structure

```
/src
   /AesGcmEncryption
       AesGcmEncryptionService.cs
       IAesGcmEncryptionService.cs
   /AesGcmEncryption.Helpers
       EncryptionHelper.cs

/test
   /AesGcmEncryption.Tests
       AesGcmEncryptionServiceTests.cs
```

## License

This project is licensed under the MIT License. See the LICENSE file for more details.
