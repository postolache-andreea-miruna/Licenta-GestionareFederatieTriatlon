using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace GestionareFederatieTriatlon.Entitati
{
    public class GestionareFederatieTriatlonContext : IdentityDbContext<Utilizator, Rol, string, IdentityUserClaim<string>,
        RoluriUtilizator, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public GestionareFederatieTriatlonContext(DbContextOptions<GestionareFederatieTriatlonContext> optiuni) : base(optiuni) { }
        public DbSet<Club> Cluburi { get; set; }
        public DbSet<Utilizator> Utilizatori { get; set; }
        public DbSet<Rol> Roluri { get; set; }
        public DbSet<RoluriUtilizator> RoluriUtilizatori { get; set; }
       // public DbSet<Chat> Chaturi { get; set; }
        public DbSet<Comentariu> Comentarii { get; set; }
       // public DbSet<Conversatie> Conversatii { get; set; }
        public DbSet<Notificare> Notificari { get; set; }
        public DbSet<Postare> Postari { get; set; }

        public DbSet<Recenzie> Recenzii { get; set; }
        public DbSet<Sportiv> Sportivi { get; set; }
        public DbSet<Antrenor> Antrenori { get; set; }
        public DbSet<Formular> Formulare { get; set; }//initial Fomulare
        public DbSet<Competitie> Competitii { get; set; }
        public DbSet<Tip> Tipuri { get; set; }
        public DbSet<Videoclip> Videoclipuri { get; set; }
        public DbSet<Locatie> Locatii { get; set; }
        public DbSet<Proba> Probe { get; set; }
        public DbSet<Istoric> Istorice { get; set; }

        public DbSet<Mesaj> Mesaje { get; set; }//nou

        public DbSet<IstoricClub> IstoriceCluburi { get; set; } 
        public DbSet<ReactiePostare> ReactiiPostari { get; set; }//cel mai nou
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Utilizator>(b =>
            {
                b.HasMany(u => u.RoluriUtilizatori)
                    .WithOne(u => u.Utilizator)
                    .HasForeignKey(ru => ru.UserId)
                    .IsRequired();
            });

            builder.Entity<Rol>(b =>
            {
                b.HasMany(r => r.RoluriUtilizatori)
                    .WithOne(r => r.Rol)
                    .HasForeignKey(ru => ru.RoleId)
                    .IsRequired();
            });

            builder.Entity<Utilizator>()

                .HasOne(u => u.AntrenorUtilizator)
                .WithMany()
                .HasForeignKey(a => a.codAntrenor);
               
             

            //Relatia 1-M (Club - Utilizator)
            builder.Entity<Club>()
                .HasMany(c => c.Utilizatori)
                .WithOne(u => u.Club);

            //Relatia 1-M (Utilizator - Notificare)
            builder.Entity<Utilizator>()
                .HasMany(u => u.Notificari)
                .WithOne(n => n.Utilizator);

            //Relatia 1-M (Utilizator - Mesaj) nou adaugat
            builder.Entity<Utilizator>()
                .HasMany(u => u.Mesaje)
                .WithOne(m => m.Utilizator);
            

            //Relatia M-M (Utilizator - Chat)
/*            builder.Entity<Conversatie>()
                .HasKey(c => new { c.codConversatie, c.codUtilizator, c.codChat });

            builder.Entity<Conversatie>()
                .HasOne(c => c.Utilizator)
                .WithMany(u => u.Conversatii)
                .HasForeignKey(c => c.codUtilizator);

            builder.Entity<Conversatie>()
                .HasOne(c => c.Chat)
                .WithMany(ch => ch.Conversatii)
                .HasForeignKey(c => c.codChat);*/

            //Relatia 1 - M (Utilizator - Postare)
            builder.Entity<Utilizator>()
                .HasMany(u => u.Postari)
                .WithOne(p => p.Utilizator);

            //Relatia 1 - M (Postare - Comentariu)
            builder.Entity<Comentariu>()
                .HasKey(c => new {c.codPostare, c.codComentariu});

            builder.Entity<Postare>()
                .HasMany(p => p.Comentarii)
                .WithOne(c => c.Postare);

            //////////
            //Relatia 1 - M (Sportiv - Recenzie)
            builder.Entity<Sportiv>()
                .HasMany(s => s.Recenzii)
                .WithOne(r => r.Sportiv);

            //Relatia 1 - M (Sportiv - Formular)
            builder.Entity<Sportiv>()
                .HasMany(s => s.Formulare)
                .WithOne(f => f.Sportiv);

            //Relatia 1 - M (Competitie - Recenzie)
            builder.Entity<Competitie>()
                .HasMany(c => c.Recenzii)
                .WithOne(r => r.Competitie);

            //Relatia 1 - M (Tip - Competitie)
            builder.Entity<Tip>()
                .HasMany(t => t.Competitii)
                .WithOne(t => t.Tip);

            //Relatia 1 - 1 (Competitie - Videoclip)
            builder.Entity<Competitie>()
                .HasOne(c => c.Videoclip)
                .WithOne(v => v.Competitie);

            //Relatia 1 - M (Locatie - Competitie)
            builder.Entity<Locatie>()
                .HasMany(l => l.Competitii)
                .WithOne(c => c.Locatie);

//------------------------------------------------------
            //cheie primara compusa
            builder.Entity<IstoricClub>()
                .HasKey(ic => new { ic.codUtilizator, ic.codClub, ic.dataInscriereClub });

            //Relatia 1-M (Club - IstoricClub)
            builder.Entity<IstoricClub>()
                .HasOne(ic => ic.Club)
                .WithMany(c => c.IstoriceCluburi)
                .HasForeignKey(ic => ic.codClub)
                .OnDelete(DeleteBehavior.NoAction);

            //Relatia 1-M (Utilizator - IstoricClub)
            builder.Entity<IstoricClub>()
                .HasOne(ic => ic.Utilizator)
                .WithMany(u => u.IstoriceCluburi)
                .HasForeignKey(ic => ic.codUtilizator);
            //------------------------------------------------------

            //------------------------------------------------------

            builder.Entity<ReactiePostare>()
                .HasKey(rp => new { rp.codUtilizator, rp.codPostare });

            //relatie 1-M (Utilizator - ReactiePostare)
            builder.Entity<ReactiePostare>()
                .HasOne(rp => rp.Utilizator)
                .WithMany(u => u.ReactiiPostari)
                .HasForeignKey(rp => rp.codUtilizator);

            //relatia 1-M (Postare - ReactiePostare)
            builder.Entity<ReactiePostare>()
                .HasOne(rp => rp.Postare)
                .WithMany(p => p.ReactiiPostari)
                .HasForeignKey(rp => rp.codPostare)
                .OnDelete(DeleteBehavior.NoAction); ;

//------------------------------------------------------

            //Relatia M - M - M (Sportiv - Proba - Competitie)
            builder.Entity<Istoric>()
                .HasKey(i => new {i.codUtilizator, i.codProba, i.codCompetitie});

            builder.Entity<Istoric>()
                .HasOne(i => i.Sportiv)
                .WithMany(s => s.Istorice)
                .HasForeignKey(i => i.codUtilizator);

            builder.Entity<Istoric>()
                .HasOne(i => i.Proba)
                .WithMany(p => p.Istorice)
                .HasForeignKey(i => i.codProba);

            builder.Entity<Istoric>()
                .HasOne(i => i.Competitie)
                .WithMany(c => c.Istorice)
                .HasForeignKey(i => i.codCompetitie);
        }
    }
}
