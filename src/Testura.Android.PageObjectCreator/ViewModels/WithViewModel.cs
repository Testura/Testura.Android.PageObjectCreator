using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.CommandWpf;
using PropertyChanged;
using Testura.Android.Device.Ui.Nodes.Data;
using Testura.Android.PageObjectCreator.Models;
using Testura.Android.PageObjectCreator.Services;
using Testura.Android.Util;
using Attribute = Testura.Android.PageObjectCreator.Models.Attribute;

namespace Testura.Android.PageObjectCreator.ViewModels
{
    [ImplementPropertyChanged]
    public class WithViewModel
    {
        private IList<Node> _allNodes;
        private readonly IOptimalWithService _optimalWithService;

        public WithViewModel(IOptimalWithService optimalWithService)
        {
            _allNodes = new List<Node>();
            _optimalWithService = optimalWithService;
            NotUsedWiths = new ObservableCollection<AttributeTags>();
            UsedWiths = new ObservableCollection<AttributeTags>();
            AddCommand = new RelayCommand<AttributeTags>(AddWith);
            RemoveCommand = new RelayCommand<AttributeTags>(RemoveWith);
            OkCommand = new RelayCommand(Save);
            CancelCommand = new RelayCommand(Close);
            Attributes = new ObservableCollection<Attribute>();
        }

        public event EventHandler CloseWindow;

        public bool UseUniqueWiths { get; set; }

        public ObservableCollection<AttributeTags> NotUsedWiths { get; set; }

        public ObservableCollection<AttributeTags> UsedWiths { get; set; }

        public RelayCommand<AttributeTags> AddCommand { get; set; }

        public RelayCommand<AttributeTags> RemoveCommand { get; set; }

        public RelayCommand OkCommand { get; set; }

        public RelayCommand CancelCommand { get; set; }

        public UiObjectInfo UiObjectInfo { get; set; }

        public ObservableCollection<Attribute> Attributes { get; set; }

        public void SetCurrentUiObjectInfo(UiObjectInfo uiObjectInfo, IList<Node> allNodes)
        {
            _allNodes = new List<Node>(allNodes);
            UiObjectInfo = uiObjectInfo;
            UseUniqueWiths = uiObjectInfo.Optimal != null;
            NotUsedWiths.Clear();
            UsedWiths.Clear();
            LoadWiths();
            LoadAttributes();
        }

        private void LoadAttributes()
        {
            Attributes.Clear();
            Attributes.Add(new Attribute("Index", UiObjectInfo.Node.Index));
            Attributes.Add(new Attribute("Text", UiObjectInfo.Node.Text));
            Attributes.Add(new Attribute("Resource-id", UiObjectInfo.Node.ResourceId));
            Attributes.Add(new Attribute("Class", UiObjectInfo.Node.Class));
            Attributes.Add(new Attribute("Package", UiObjectInfo.Node.Package));
            Attributes.Add(new Attribute("Content-desc", UiObjectInfo.Node.ContentDesc));
        }

        private void LoadWiths()
        {
            CheckWith(UiObjectInfo.Node.Text, AttributeTags.Text);
            CheckWith(UiObjectInfo.Node.ResourceId, AttributeTags.ResourceId);
            CheckWith(UiObjectInfo.Node.Class, AttributeTags.Class);
            CheckWith(UiObjectInfo.Node.ContentDesc, AttributeTags.ContentDesc);
            CheckWith(UiObjectInfo.Node.Package, AttributeTags.Package);
            CheckWith(UiObjectInfo.Node.Index, AttributeTags.Index);
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

            if (UseUniqueWiths)
            {
                UiObjectInfo.Optimal = _optimalWithService.GetOptimalWith(UiObjectInfo.Node, _allNodes);
            }
            else
            {
                UiObjectInfo.Optimal = null;
            }

            UiObjectInfo.FindWithString = UiObjectInfo.Optimal != null ? "Automatic" : string.Join(", ", UiObjectInfo.FindWith);
            Close();
        }

        private void Close()
        {
            CloseWindow?.Invoke(this, new EventArgs());
        }
    }
}