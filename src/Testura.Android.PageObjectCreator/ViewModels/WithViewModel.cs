using System;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.CommandWpf;
using PropertyChanged;
using Testura.Android.PageObjectCreator.Models;
using Testura.Android.Util;
using Attribute = Testura.Android.PageObjectCreator.Models.Attribute;

namespace Testura.Android.PageObjectCreator.ViewModels
{
    [ImplementPropertyChanged]
    public class WithViewModel
    {
        public WithViewModel()
        {
            NotUsedWiths = new ObservableCollection<AttributeTags>();
            UsedWiths = new ObservableCollection<AttributeTags>();
            AddCommand = new RelayCommand<AttributeTags>(AddWith);
            RemoveCommand = new RelayCommand<AttributeTags>(RemoveWith);
            OkCommand = new RelayCommand(Save);
            CancelCommand = new RelayCommand(Close);
            Attributes = new ObservableCollection<Attribute>();
        }

        public event EventHandler CloseWindow;

        public ObservableCollection<AttributeTags> NotUsedWiths { get; set; }

        public ObservableCollection<AttributeTags> UsedWiths { get; set; }

        public RelayCommand<AttributeTags> AddCommand { get; set; }

        public RelayCommand<AttributeTags> RemoveCommand { get; set; }

        public RelayCommand OkCommand { get; set; }

        public RelayCommand CancelCommand { get; set; }

        public UiObjectInfo UiObjectInfo { get; set; }

        public ObservableCollection<Attribute> Attributes { get; set; }

        public void SetCurrentUiObjectInfo(UiObjectInfo uiObjectInfo)
        {
            UiObjectInfo = uiObjectInfo;
            NotUsedWiths.Clear();
            UsedWiths.Clear();
            LoadWiths();
            LoadAttributes();
        }

        private void LoadAttributes()
        {
            Attributes.Clear();
            Attributes.Add(new Attribute("Index", UiObjectInfo.AndroidElement.Index));
            Attributes.Add(new Attribute("Text", UiObjectInfo.AndroidElement.Text));
            Attributes.Add(new Attribute("Resource-id", UiObjectInfo.AndroidElement.ResourceId));
            Attributes.Add(new Attribute("Class", UiObjectInfo.AndroidElement.Class));
            Attributes.Add(new Attribute("Package", UiObjectInfo.AndroidElement.Package));
            Attributes.Add(new Attribute("Content-desc", UiObjectInfo.AndroidElement.ContentDesc));
        }

        private void LoadWiths()
        {
            CheckWith(UiObjectInfo.AndroidElement.Text, AttributeTags.Text);
            CheckWith(UiObjectInfo.AndroidElement.ResourceId, AttributeTags.ResourceId);
            CheckWith(UiObjectInfo.AndroidElement.Class, AttributeTags.Class);
            CheckWith(UiObjectInfo.AndroidElement.ContentDesc, AttributeTags.ContentDesc);
            CheckWith(UiObjectInfo.AndroidElement.Package, AttributeTags.Package);
            CheckWith(UiObjectInfo.AndroidElement.Index, AttributeTags.Index);
        }

        private void CheckWith(string value, AttributeTags tag)
        {
            if (UiObjectInfo.FindWith.Contains(tag))
            {
                UsedWiths.Add(tag);
            }
            else
            {
                if (string.IsNullOrEmpty(value))
                {
                    return;
                }

                NotUsedWiths.Add(tag);
            }
        }

        private void AddWith(AttributeTags tag)
        {
            if (tag == AttributeTags.TextContains)
            {
                return;
            }

            UsedWiths.Add(tag);
            NotUsedWiths.Remove(tag);
        }

        private void RemoveWith(AttributeTags tag)
        {
            if (tag == AttributeTags.TextContains)
            {
                return;
            }

            UsedWiths.Remove(tag);
            NotUsedWiths.Add(tag);
        }

        private void Save()
        {
            UiObjectInfo.FindWith.Clear();
            foreach (var with in UsedWiths)
            {
                UiObjectInfo.FindWith.Add(with);
            }

            UiObjectInfo.FindWithString = string.Join(", ", UiObjectInfo.FindWith);
            Close();
        }


        private void Close()
        {
            CloseWindow?.Invoke(this, new EventArgs());
        }
    }
}