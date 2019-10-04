﻿using System;
using System.Threading.Tasks;
using DataLayer;
using Dinah.Core.ErrorHandling;
using FileManager;

namespace DomainServices
{
    /// <summary>
    /// Download DRM book and decrypt audiobook files.
    /// 
    /// Processes:
    /// Download: download aax file: the DRM encrypted audiobook
    /// Decrypt: remove DRM encryption from audiobook. Store final book
    /// Backup: perform all steps (downloaded, decrypt) still needed to get final book
    /// </summary>
    public class BackupBook : IProcessable
    {
        public event EventHandler<string> Begin;
        public event EventHandler<string> StatusUpdate;
        public event EventHandler<string> Completed;

        public DownloadBook Download { get; } = new DownloadBook();
        public DecryptBook Decrypt { get; } = new DecryptBook();

        // ValidateAsync() doesn't need UI context
        public async Task<bool> ValidateAsync(LibraryBook libraryBook)
            => await validateAsync_ConfigureAwaitFalse(libraryBook.Book.AudibleProductId).ConfigureAwait(false);
        private async Task<bool> validateAsync_ConfigureAwaitFalse(string productId)
            => !await AudibleFileStorage.Audio.ExistsAsync(productId);

        // do NOT use ConfigureAwait(false) on ProcessUnregistered()
        // often does a lot with forms in the UI context
        public async Task<StatusHandler> ProcessAsync(LibraryBook libraryBook)
        {
            var displayMessage = $"[{libraryBook.Book.AudibleProductId}] {libraryBook.Book.Title}";

            Begin?.Invoke(this, displayMessage);

            try
            {
                var aaxExists = await AudibleFileStorage.AAX.ExistsAsync(libraryBook.Book.AudibleProductId);
                if (!aaxExists)
                    await Download.ProcessAsync(libraryBook);

                return await Decrypt.ProcessAsync(libraryBook);
            }
            finally
            {
                Completed?.Invoke(this, displayMessage);
            }
        }
    }
}
