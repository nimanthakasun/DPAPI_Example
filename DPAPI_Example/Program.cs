using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

class SecureCredentials
{
    private static string filePath = "encypteddata.dat";

    // Encrypt and save to file
    public static void EncryptAndSave(string inputData)
    {
        string combined = inputData;
        byte[] plaintextBytes = Encoding.UTF8.GetBytes(combined);

        byte[] encryptedBytes = ProtectedData.Protect(
            plaintextBytes,
            null, // Can be null
            DataProtectionScope.CurrentUser
        );

        File.WriteAllBytes(filePath, encryptedBytes);
        Console.WriteLine("Encrypted and saved.");
    }

    // Read from file and decrypt
    public static void LoadAndDecrypt()
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine("Encrypted file not found.");
            return;
        }

        byte[] encryptedBytes = File.ReadAllBytes(filePath);

        byte[] decryptedBytes = ProtectedData.Unprotect(
            encryptedBytes,
            null,
            DataProtectionScope.CurrentUser
        );

        string textOutput = Encoding.UTF8.GetString(decryptedBytes);

        Console.WriteLine($"Data: {textOutput}");
    }

    static void Main()
    {
        Console.Write("Enter the text you want to encrypt using DPAPI: ");
        string data = Console.ReadLine();

        EncryptAndSave(data);
        Console.WriteLine("\nNow trying to load and decrypt:");
        LoadAndDecrypt();
    }
}

