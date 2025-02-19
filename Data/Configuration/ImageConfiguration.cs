using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace assnet8.Data.Configuration
{
       public class ImageConfiguration : IEntityTypeConfiguration<Image>
       {
              public void Configure(EntityTypeBuilder<Image> builder)
              {
                     builder.HasIndex(i => i.S3Id).IsUnique();
                     // // Configure one-to-one relationship with User (ProfileImage)
                     // builder.HasOne(i => i.ProfileImageUser)
                     //        .WithOne(u => u.ProfileImage)
                     //        .HasForeignKey<User>(u => u.ProfileImageId)
                     //        .OnDelete(DeleteBehavior.NoAction); // Set ProfileImageId to null when Image is deleted

                     // Configure one-to-many relationship with User (UploadedImages)
                     builder.HasOne(i => i.UploadedImagesUser)
                            .WithMany(u => u.UploadedImages)
                            .HasForeignKey(i => i.UserId)
                            .OnDelete(DeleteBehavior.Cascade); // Delete all UploadedImages when User is deleted

                     builder.HasOne(i => i.Gallery)
                            .WithMany(g => g.Images)
                            .HasForeignKey(i => i.GalleryId)
                            .OnDelete(DeleteBehavior.Cascade);

                     // // Configure one-to-one relationship with Field
                     // builder.HasOne(i => i.Field)
                     //        .WithOne(f => f.ThumbnailImage)
                     //        .HasForeignKey<Field>(f => f.ThumbnailImageId)
                     //        .OnDelete(DeleteBehavior.NoAction); // Set ImageId to null in Field when Image is deleted

                     // // Configure one-to-one relationship with Listing
                     // builder.HasOne(i => i.Listing)
                     //        .WithOne(l => l.ThumbnailImage)
                     //        .HasForeignKey<Listing>(l => l.ThumbnailImageId)
                     //        .OnDelete(DeleteBehavior.NoAction); // Set ImageId to null in Listing when Image is deleted

                     // // Configure one-to-one relationship with Service
                     // builder.HasOne(i => i.Service)
                     //        .WithOne(s => s.ThumbnailImage)
                     //        .HasForeignKey<Service>(s => s.ThumbnailImageId)
                     //        .OnDelete(DeleteBehavior.NoAction); // Set ImageId to null in Service when Image is deleted

                     // // Configure one-to-one relationship with Team
                     // builder.HasOne(i => i.Team)
                     //        .WithOne(t => t.LogoImage)
                     //        .HasForeignKey<Team>(t => t.LogoImageId)
                     //        .OnDelete(DeleteBehavior.NoAction); // Set ImageId to null in Team when Image is deleted

                     // // Configure one-to-one relationship with Organization
                     // builder.HasOne(i => i.Organization)
                     //        .WithOne(o => o.LogoImage)
                     //        .HasForeignKey<Organization>(o => o.LogoImageId)
                     //        .OnDelete(DeleteBehavior.NoAction); // Set ImageId to null in Organization when Image is deleted

              }
       }
}