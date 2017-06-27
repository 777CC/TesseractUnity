﻿using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
//using System.Threading.Tasks;
//using Android.Content;
//using Android.Graphics;
//using Android.Util;
//using Com.Googlecode.Tesseract.Android;
//using Java.Lang;
using Exception = System.Exception;
//using File = Java.IO.File;
//using Object = Java.Lang.Object;

namespace Tesseract
{
    public class TesseractApi
    {
        /// <summary>
        ///     Whitelist of characters to recognize.
        /// </summary>
        public const string VAR_CHAR_WHITELIST = "tessedit_char_whitelist";

        /// <summary>
        ///     Blacklist of characters to not recognize.
        /// </summary>
        public const string VAR_CHAR_BLACKLIST = "tessedit_char_blacklist";

        //private readonly AssetsDeployment _assetsDeployment;
        //private readonly Context _context;
        //private readonly ProgressHandler _progressHandler = new ProgressHandler();
        //private TessBaseAPI _api;
        //private volatile bool _busy;
        //private Rectangle? _rect;

        //public TesseractApi(Context context, AssetsDeployment assetsDeployment)
        public TesseractApi()
        {
            
            // jni.FindClass("java.lang.String");
            // jni.GetMethodID(classID, "<init>", "(Ljava/lang/String;)V");
            // jni.NewStringUTF("some_string");
            // jni.NewObject(classID, methodID, javaString); 
            //int hash = jo.Call<int>("hashCode");
            // jni.GetMethodID(classID, "hashCode", "()I"); 
            // jni.CallIntMethod(objectID, methodID);

            //_assetsDeployment = assetsDeployment;
            //_context = context;
            //_progressHandler.Progress += (sender, e) => {
            //    OnProgress(e.Progress);
            //};
            //_api = new TessBaseAPI(_progressHandler);
        }


        //public BitmapFactory.Options Options { get; set; } = new BitmapFactory.Options { InSampleSize = 1 };

        //public string Text { get; private set; }

        //public bool Initialized { get; private set; }

        //public async Task<bool> Init(string language, OcrEngineMode? mode = null)
        //{
        //    if (string.IsNullOrEmpty(language))
        //        return false;
        //    try
        //    {
        //        var path = await CopyAssets();
        //        var result = mode.HasValue
        //            ? _api.Init(path, language, GetOcrEngineMode(mode.Value))
        //            : _api.Init(path, language);
        //        Initialized = result;
        //        return result;
        //    }
        //    catch (IllegalArgumentException ex)
        //    {
        //        Log.Debug("TesseractApi", ex, ex.Message);
        //        Initialized = false;
        //        return false;
        //    }
        //}

        //public void SetVariable(string key, string value)
        //{
        //    CheckIfInitialized();
        //    _api.SetVariable(key, value);
        //}

        //public async Task<bool> SetImage(byte[] data)
        //{
        //    CheckIfInitialized();
        //    if (data == null)
        //        throw new ArgumentNullException(nameof(data));
        //    using (var bitmap = await BitmapFactory.DecodeByteArrayAsync(data, 0, data.Length, Options))
        //    {
        //        return await Recognise(bitmap);
        //    }
        //}

        //public async Task<bool> SetImage(string path)
        //{
        //    CheckIfInitialized();
        //    if (path == null)
        //        throw new ArgumentNullException(nameof(path));
        //    using (var bitmap = await BitmapFactory.DecodeFileAsync(path, Options))
        //    {
        //        return await Recognise(bitmap);
        //    }
        //}

        //public async Task<bool> SetImage(Stream stream)
        //{
        //    CheckIfInitialized();
        //    if (stream == null)
        //        throw new ArgumentNullException(nameof(stream));
        //    using (var bitmap = await BitmapFactory.DecodeStreamAsync(stream, null, Options))
        //    {
        //        return await Recognise(bitmap);
        //    }
        //}

        //public void SetWhitelist(string whitelist)
        //{
        //    CheckIfInitialized();
        //    _api.SetVariable(VAR_CHAR_WHITELIST, whitelist);
        //}

        //public void SetBlacklist(string blacklist)
        //{
        //    CheckIfInitialized();
        //    _api.SetVariable(VAR_CHAR_BLACKLIST, blacklist);
        //}

        //public void SetRectangle(Rectangle? rect)
        //{
        //    CheckIfInitialized();
        //    _rect = rect;
        //}

        //public void SetPageSegmentationMode(PageSegmentationMode mode)
        //{
        //    CheckIfInitialized();
        //    switch (mode)
        //    {
        //        case PageSegmentationMode.Auto:
        //            _api.SetPageSegMode(PageSegMode.Auto);
        //            break;
        //        case PageSegmentationMode.AutoOnly:
        //            _api.SetPageSegMode(PageSegMode.AutoOnly);
        //            break;
        //        case PageSegmentationMode.AutoOsd:
        //            _api.SetPageSegMode(PageSegMode.AutoOsd);
        //            break;
        //        case PageSegmentationMode.CircleWord:
        //            _api.SetPageSegMode(PageSegMode.CircleWord);
        //            break;
        //        case PageSegmentationMode.OsdOnly:
        //            _api.SetPageSegMode(PageSegMode.OsdOnly);
        //            break;
        //        case PageSegmentationMode.SingleBlock:
        //            _api.SetPageSegMode(PageSegMode.SingleBlock);
        //            break;
        //        case PageSegmentationMode.SingleBlockVertText:
        //            _api.SetPageSegMode(PageSegMode.SingleBlockVertText);
        //            break;
        //        case PageSegmentationMode.SingleChar:
        //            _api.SetPageSegMode(PageSegMode.SingleChar);
        //            break;
        //        case PageSegmentationMode.SingleColumn:
        //            _api.SetPageSegMode(PageSegMode.SingleColumn);
        //            break;
        //        case PageSegmentationMode.SingleLine:
        //            _api.SetPageSegMode(PageSegMode.Auto);
        //            break;
        //        case PageSegmentationMode.SingleWord:
        //            _api.SetPageSegMode(PageSegMode.SingleWord);
        //            break;
        //        case PageSegmentationMode.SparseText:
        //            _api.SetPageSegMode(PageSegMode.SparseText);
        //            break;
        //        case PageSegmentationMode.SparseTextOsd:
        //            _api.SetPageSegMode(PageSegMode.SparseTextOsd);
        //            break;
        //    }
        //}

        //public void Dispose()
        //{
        //    if (_api != null)
        //    {
        //        _api.Dispose();
        //        _api = null;
        //    }
        //}

        //public IEnumerable<Result> Results(Tesseract.PageIteratorLevel level)
        //{
        //    CheckIfInitialized();
        //    var pageIteratorLevel = GetPageIteratorLevel(level);
        //    var iterator = _api.ResultIterator;
        //    if (iterator == null)
        //        yield break;
        //    // ReSharper disable once TooWideLocalVariableScope
        //    int[] boundingBox;
        //    iterator.Begin();
        //    do
        //    {
        //        boundingBox = iterator.GetBoundingBox(pageIteratorLevel);
        //        var result = new Result
        //        {
        //            Confidence = iterator.Confidence(pageIteratorLevel),
        //            Text = iterator.GetUTF8Text(pageIteratorLevel),
        //            Box = new Rectangle(boundingBox[0], boundingBox[1], boundingBox[2] - boundingBox[0], boundingBox[3] - boundingBox[1])
        //        };
        //        yield return result;
        //    } while (iterator.Next(pageIteratorLevel));
        //}

        //public event EventHandler<ProgressEventArgs> Progress;

        //public void Clear()
        //{
        //    _rect = null;
        //    _api.Clear();
        //}

        //public async Task<bool> Init(string tessDataPath, string language)
        //{
        //    var result = _api.Init(tessDataPath, language);
        //    Initialized = result;
        //    return result;
        //}

        //public async Task<bool> Recognise(Bitmap bitmap)
        //{
        //    CheckIfInitialized();
        //    if (bitmap == null)
        //        throw new ArgumentNullException(nameof(bitmap));
        //    if (_busy)
        //        return false;
        //    _busy = true;
        //    try
        //    {
        //        await Task.Run(() => {
        //            _api.SetImage(bitmap);
        //            if (_rect.HasValue)
        //            {
        //                _api.SetRectangle((int)_rect.Value.Left, (int)_rect.Value.Top, (int)_rect.Value.Width,
        //                    (int)_rect.Value.Height);
        //            }
        //            Text = _api.UTF8Text;
        //        });
        //        return true;
        //    }
        //    finally
        //    {
        //        _busy = false;
        //    }
        //}

        //private int GetOcrEngineMode(OcrEngineMode mode)
        //{
        //    switch (mode)
        //    {
        //        case OcrEngineMode.CubeOnly:
        //            return OcrMode.CubeOnly;
        //        case OcrEngineMode.TesseractCubeCombined:
        //            return OcrMode.TesseractCubeCombined;
        //        case OcrEngineMode.TesseractOnly:
        //            return OcrMode.TesseractOnly;
        //        default:
        //            return OcrMode.CubeOnly;
        //    }
        //}

        //private int GetPageIteratorLevel(Tesseract.PageIteratorLevel level)
        //{
        //    switch (level)
        //    {
        //        case Tesseract.PageIteratorLevel.Block:
        //            return PageIteratorLevel.Block;
        //        case Tesseract.PageIteratorLevel.Paragraph:
        //            return PageIteratorLevel.Paragraph;
        //        case Tesseract.PageIteratorLevel.Symbol:
        //            return PageIteratorLevel.Symbol;
        //        case Tesseract.PageIteratorLevel.Textline:
        //            return PageIteratorLevel.Textline;
        //        case Tesseract.PageIteratorLevel.Word:
        //            return PageIteratorLevel.Word;
        //        default:
        //            return PageIteratorLevel.Word;
        //    }
        //}

        //private async Task<string> CopyAssets()
        //{
        //    try
        //    {
        //        var assetManager = _context.Assets;
        //        var files = assetManager.List("tessdata");
        //        var file = _context.GetExternalFilesDir(null);
        //        var tessdata = new File(_context.GetExternalFilesDir(null), "tessdata");
        //        if (!tessdata.Exists())
        //        {
        //            tessdata.Mkdir();
        //        }
        //        else if (_assetsDeployment == AssetsDeployment.OncePerVersion)
        //        {
        //            var packageInfo = _context.PackageManager.GetPackageInfo(_context.PackageName, 0);
        //            var version = packageInfo.VersionName;
        //            var versionFile = new File(tessdata, "version");
        //            if (versionFile.Exists())
        //            {
        //                var fileVersion = System.IO.File.ReadAllText(versionFile.AbsolutePath);
        //                if (version == fileVersion)
        //                {
        //                    Log.Debug("TesseractApi", "Application version didn't change, skipping copying assets");
        //                    return file.AbsolutePath;
        //                }
        //                versionFile.Delete();
        //            }
        //            System.IO.File.WriteAllText(versionFile.AbsolutePath, version);
        //        }

        //        Log.Debug("TesseractApi", "Copy assets to " + file.AbsolutePath);

        //        foreach (var filename in files)
        //        {
        //            using (var inStream = assetManager.Open("tessdata/" + filename))
        //            {
        //                var outFile = new File(tessdata, filename);
        //                if (outFile.Exists())
        //                {
        //                    outFile.Delete();
        //                }
        //                using (var outStream = new FileStream(outFile.AbsolutePath, FileMode.Create))
        //                {
        //                    await inStream.CopyToAsync(outStream);
        //                    await outStream.FlushAsync();
        //                }
        //            }
        //        }
        //        return file.AbsolutePath;
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error("TesseractApi", ex.Message);
        //    }
        //    return null;
        //}

        //private void OnProgress(int progress)
        //{
        //    var handler = Progress;
        //    handler?.Invoke(this, new ProgressEventArgs(progress));
        //}

        //private void CheckIfInitialized()
        //{
        //    if (!Initialized)
        //        throw new InvalidOperationException("Call Init first");
        //}

        //private class ProgressHandler : Object, TessBaseAPI.IProgressNotifier
        //{
        //    public void OnProgressValues(TessBaseAPI.ProgressValues progress)
        //    {
        //        OnProgress(progress.Percent);
        //    }

        //    internal event EventHandler<ProgressEventArgs> Progress;

        //    private void OnProgress(int progress)
        //    {
        //        var handler = Progress;
        //        handler?.Invoke(this, new ProgressEventArgs(progress));
        //    }
        //}
    }
}