using System;
using System.IO;
using Microsoft.Extensions.Logging;
using TestMovieSite.Domain.Helpers;
using TestMovieSite.DTO;

namespace TestMovieSite.Domain.Storage
{
    public class LocalStorage : Models.Storage, IStorage
    {
        private readonly ILogger<LocalStorage> _logger;

        public LocalStorage(ILogger<LocalStorage> logger)
        {
            _logger = logger;
        }
        
        public LocalStorage(Models.Storage storage, ILogger<LocalStorage> logger)
        {
            Id = storage.Id;
            Type = storage.Type;
            StoragePath = storage.StoragePath;
            IsDefault = storage.IsDefault;

            _logger = logger;
        }
        
        public OperationResult<byte[]> TryDownloadFile(string filePath)
        {
            try
            {
                using var fStream = new FileStream(Path.Combine(StoragePath, filePath), FileMode.OpenOrCreate);
                var fileData = new byte[fStream.Length]; 
                fStream.Read(fileData, 0, fileData.Length);
                return new OperationResult<byte[]>(data:fileData, isSuccess: true);
            }
            catch (Exception e)
            {
                _logger.LogError(e,"Failed to download file.");
                return new OperationResult<byte[]>(data:null, isSuccess: false);
            }
        }

        public OperationResult<string> TryUploadFile(FileDto fileDto)
        {
            try
            {
                var currentDirectory = Directory.GetCurrentDirectory();
                var storageDirectory = Path.Combine(currentDirectory, "wwwroot", StoragePath);
                if(!Directory.Exists(storageDirectory))
                {
                    Directory.CreateDirectory(storageDirectory);
                }
                var filePath = Path.Combine(storageDirectory, fileDto.Name);
                //Что нужно улучшить: брать hash от содержимого файла вместо использования имени файла передаваемого с фронта
                //код для работы м директориями можно убрать заменив точным путем к хранилищу, но тогда для запуска на новой машине нужно было бы создававть вручную директории
                
                using var fStream = new FileStream(filePath, FileMode.OpenOrCreate);
                fStream.Write(fileDto.FileData, 0, fileDto.FileData.Length);

                var relativePath = Path.Combine("\\",StoragePath, fileDto.Name);
                return new OperationResult<string>(data: relativePath, isSuccess: true);
                
                // Что нужно улучшить: 
                // Сейчас этот метод возвращает точный путь к файлу на внутри wwwroot.
                // Просто возвратом пути к фалу можно ограничится при использовании облачных хранилищ
                // Для локального хотел еще реализовать возращение относительной ссылки виды "/getfile/{тип хранилиша}/{имя файла}"
                // По этой ссылке отрабатывал бы метод FileController.GetFile, который бы использовал FileService.GetFile, возвращающий byte[]
                // Также можно добавить  сжатие картинок до заданного разрешения и отдельно маленькие превью, которые показывать при отображении списка фильмов
                // Реализовать могу, но потребует еще времени, которого я и так на тестовое достаточно потратил. 
                // При этом каких-то ВАУ-штук код бы не содержал
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to upload file.");
                return new OperationResult<string>(data:null, isSuccess: false);
            }
        }

    }
}