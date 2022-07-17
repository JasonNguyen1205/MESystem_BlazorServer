using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace MESystem.SEO
{
    public class MetadataTransferService : INotifyPropertyChanged, IDisposable
    {
        private readonly NavigationManager _navigationManager;
        public event PropertyChangedEventHandler PropertyChanged;
        private List<MetadataValue> MetadataValues {get; set;}

        private string _title;

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        private string _description;

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new(propertyName));
        }

        public MetadataTransferService(NavigationManager navigationManager)
        {
            LoadMetadataValues();
            _navigationManager = navigationManager;

            // Susbscribe to the location changed event
            _navigationManager.LocationChanged += UpdateMetadata;

            // Initial Call
            UpdateMetadata(_navigationManager.Uri);
        }

        private void UpdateMetadata(object sender, LocationChangedEventArgs e)
        {
            UpdateMetadata(e.Location);
        }
        
        private void UpdateMetadata(string url)
        {
            var metadataValue = MetadataValues.FirstOrDefault(mv => url.EndsWith(mv.Url));

            if (metadataValue is null)
            {
                metadataValue = new()
                {
                    Title = "Default",
                    Description = "Default"
                };
            }

            Title = metadataValue.Title;
            Description = metadataValue.Description;
        }
        private void LoadMetadataValues()
        {
            MetadataValues = new List<MetadataValue>();

               MetadataValues.Add(
                new()
                {
                    Url = "/",
                    Title = "FRIWO System - Home",
                    Description = "FRIWO System - Home page"
                });

            MetadataValues.Add(
                new()
                {
                    Url = "/login",
                    Title = "FRIWO System - Login",
                    Description = "FRIWO System - Login Page"
                });

            MetadataValues.Add(
                new()
                {
                    Url = "/profile",
                    Title = "FRIWO System - Profile",
                    Description = "This is a profile page for user."
                });

            MetadataValues.Add(
                new()
                {
                    Url = "/contacts",
                    Title = "FRIWO System - Contact",
                    Description = "This is a contacts page for FSystem user."
                });

            MetadataValues.Add(
                new()
                {
                    Url = "/settings",
                    Title = "FRIWO System - Settings",
                    Description = "This is a settings page for user."
                });
            MetadataValues.Add(
                new()
                {
                    Url = "/createaccount",
                    Title = "FRIWO System - User Register",
                    Description = "This is a settings page for registring user."
                });
            MetadataValues.Add(
                new()
                {
                    Url = "/chat",
                    Title = "FRIWO System - Chat",
                    Description = "This is a page for communication between Users."
                });
            MetadataValues.Add(
               new()
               {
                   Url = "/warehouse/shipping",
                   Title = "FRIWO System - Shipping",
                   Description = "This is a page for shipping function."
               });
            MetadataValues.Add(
               new()
               {
                   Url = "/shipmentoverview",
                   Title = "FRIWO System - Shipment Overview",
                   Description = "This is a page for shipping function."
               });
        }
    
        public void Dispose()
        {
            // Unsubscribe the events
            _navigationManager.LocationChanged -= UpdateMetadata;
        }
    }

    public class MetadataValue
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}