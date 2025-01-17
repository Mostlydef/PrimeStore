﻿using PrimeStore.Data.Models;

namespace PrimeStore.Data.Interfaces
{
    public interface IAllFile
    {
        IEnumerable<Models.File> Files { get; } 

        public Models.File File { set; }

        public bool SetFileInBasket(int id);
        public bool RemoveFileFromBasket(int id);
        public bool RemoveFile(int id);

        public Models.File GetFileData(int id);
    }
}
