using MediaPlannerCore.Data.Repositories;
using MediaPlannerCore.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaPlannerCore.Service.Services
{
    public class MediaChannelService : IMediaChannelService
    {
        private readonly IMediaChannelRepository mediaChannelRepository;
        public MediaChannelService(IMediaChannelRepository mediaChannelRepository)
        {
            this.mediaChannelRepository = mediaChannelRepository;
        }

        public void AddMediaChannel(MediaChannel mediaChannel)
        {
            this.mediaChannelRepository.Insert(mediaChannel);
        }
        public void UpdateMediaChannel(MediaChannel mediaChannel)
        {
            this.mediaChannelRepository.Update(mediaChannel);
        }

        public void DeleteMediaChannel(int? id)
        {
            this.mediaChannelRepository.Delete(id);
        }

        public MediaChannel GetMediaChannelById(int? id)
        {
            return this.mediaChannelRepository.GetByID(id);
        }

        public IEnumerable<MediaChannel> GetMediaChannels(string filter, string includeProperties)
        {
            IEnumerable<MediaChannel> mediaChannels = null;
            if (filter != null)
            {
                mediaChannels = this.mediaChannelRepository.Get(s => s.MediaChannelName.Contains(filter), includeProperties).AsEnumerable();

            }
            else
            {
                mediaChannels = this.mediaChannelRepository.Get(null, includeProperties).AsEnumerable();
            }
            return mediaChannels;
        }
    }
    public interface IMediaChannelService
    {
        IEnumerable<MediaChannel> GetMediaChannels(string filter, string includeProperties);
        MediaChannel GetMediaChannelById(int? id);
        void AddMediaChannel(MediaChannel mediaChannel);
        void UpdateMediaChannel(MediaChannel mediaChannel);
        void DeleteMediaChannel(int? id);

    }
}
