using GestionareFederatieTriatlon.Entitati;
using GestionareFederatieTriatlon.Migrations;
using GestionareFederatieTriatlon.Modele;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace GestionareFederatieTriatlon.Manageri
{
    public class AutentificareManager : IAutentificareManager
    {
       
        private readonly UserManager<Utilizator> utilizatorManager;
      //  private readonly UserManager<Antrenor> antrenorManager;
        private readonly SignInManager<Utilizator> logareManager;
        private readonly ITokenManager tokenManager;
        public AutentificareManager(UserManager<Utilizator> utilizatorManager,SignInManager<Utilizator> logareManager, ITokenManager tokenManager)
        {
          //  this.antrenorManager= antrenorManager;
            this.utilizatorManager = utilizatorManager;
            this.logareManager = logareManager;
            this.tokenManager = tokenManager;
        }


        public async Task Inregistrare(InregistrareUtilizatorModel inregistrareUtilizatorModel)
        {

            Regex emailRegex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", RegexOptions.IgnoreCase);
            if (emailRegex.IsMatch(inregistrareUtilizatorModel.Email) == true)
            {
                
                if (inregistrareUtilizatorModel.CodRol == "SportivUtilizator")
                {
                    var antrenor = await utilizatorManager.FindByEmailAsync(inregistrareUtilizatorModel.emailAntrenor);
                    //daca nu exista antrnor pt mailul dat,
                    //daca mailul nu este al unui antrenor (codAntrenor este completat doar la sportiv) -- se evita cazul in care se poate pune parola unui alt sportiv
                    if (antrenor == null || antrenor.codAntrenor is not null) 
                    {
                        throw new Exception();
                    }

                    var utilizator = new Sportiv //creez SportivUtilizator
                    {
                        Email = inregistrareUtilizatorModel.Email,
                        UserName = inregistrareUtilizatorModel.Email,
                        nume = inregistrareUtilizatorModel.nume,
                        prenume = inregistrareUtilizatorModel.prenume,
                        abonareStiri = inregistrareUtilizatorModel.abonareStiri,
                        urlPozaProfil = inregistrareUtilizatorModel.urlPozaProfil,
                        codAntrenor = antrenor.Id, //(string)inregistrareUtilizatorModel.codAntrenor,
                        codClub = inregistrareUtilizatorModel.codClub,
                        numarLegitimatie = (int)inregistrareUtilizatorModel.numarLegitimatie,
                        dataNastere = (DateTime)inregistrareUtilizatorModel.dataNastere,
                        gen = inregistrareUtilizatorModel.gen,
                        dataInscreireClubActual = DateTime.Now//acum

                    };

                    var resultat = await utilizatorManager.CreateAsync(utilizator, inregistrareUtilizatorModel.Parola);//se face utilizatorul cu parola care se hashuieste prin create async
                    if (resultat.Succeeded)
                    {
                        await utilizatorManager.AddToRoleAsync(utilizator, inregistrareUtilizatorModel.CodRol);//i se adauga rolul userului
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                else if (inregistrareUtilizatorModel.CodRol == "AntrenorUtilizator")
                {
                    var utilizator = new Antrenor //creez AntrenorUtilizator
                    {
                        Email = inregistrareUtilizatorModel.Email,
                        UserName = inregistrareUtilizatorModel.Email,
                        nume = inregistrareUtilizatorModel.nume,
                        prenume = inregistrareUtilizatorModel.prenume,
                        abonareStiri = inregistrareUtilizatorModel.abonareStiri,
                        urlPozaProfil = inregistrareUtilizatorModel.urlPozaProfil,
                        codAntrenor = null,
                        codClub = inregistrareUtilizatorModel.codClub,
                        gradPregatire = (string)inregistrareUtilizatorModel.gradPregatire,
                        dataInscreireClubActual = DateTime.Now
                    };

                    var resultat = await utilizatorManager.CreateAsync(utilizator, inregistrareUtilizatorModel.Parola);//se face utilizatorul cu parola care se hashuieste prin create async
                    if (resultat.Succeeded)
                    {
                        await utilizatorManager.AddToRoleAsync(utilizator, inregistrareUtilizatorModel.CodRol);//i se adauga rolul userului
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                else if (inregistrareUtilizatorModel.CodRol == "AdminUtilizator")
                {
                    var utilizator = new Utilizator //creez AdminUtilizator
                    {
                        Email = inregistrareUtilizatorModel.Email,
                        UserName = inregistrareUtilizatorModel.Email,
                        nume = inregistrareUtilizatorModel.nume,
                        prenume = inregistrareUtilizatorModel.prenume,
                        abonareStiri = inregistrareUtilizatorModel.abonareStiri,
                        urlPozaProfil = inregistrareUtilizatorModel.urlPozaProfil,
                        codAntrenor = null,
                        codClub = inregistrareUtilizatorModel.codClub,
                        dataInscreireClubActual = DateTime.Now

                    };

                    var resultat = await utilizatorManager.CreateAsync(utilizator, inregistrareUtilizatorModel.Parola);//se face utilizatorul cu parola care se hashuieste prin create async
                    if (resultat.Succeeded)
                    {
                        await utilizatorManager.AddToRoleAsync(utilizator, inregistrareUtilizatorModel.CodRol);//i se adauga rolul userului
                    }
                    else
                    {
                        throw new Exception();
                    }
                }

                /*           var utilizator = new Utilizator //creez utilizator
                           {
                               Email = inregistrareUtilizatorModel.Email,
                               UserName = inregistrareUtilizatorModel.Email,
                               nume = inregistrareUtilizatorModel.nume,
                               prenume = inregistrareUtilizatorModel.prenume,
                               abonareStiri = inregistrareUtilizatorModel.abonareStiri,
                               urlPozaProfil = inregistrareUtilizatorModel.urlPozaProfil,
                               codClub = inregistrareUtilizatorModel.codClub,
                           };*/

                /* var resultat = await utilizatorManager.CreateAsync(utilizator, inregistrareUtilizatorModel.Parola);//se face utilizatorul cu parola care se hashuieste prin create async
                 if(resultat.Succeeded)
                 {
                     await utilizatorManager.AddToRoleAsync(utilizator, inregistrareUtilizatorModel.CodRol);//i se adauga rolul userului
                 }*/
            }
            else
            {
                throw new Exception();
            }
        }

        public async Task<TokenModel> Logare(LogareUtilizatorModel logareUtilizatorModel)
        {
            //se verif daca exista userul in aplicatie
            var utilizator = await utilizatorManager.FindByEmailAsync(logareUtilizatorModel.Email);
            if( utilizator != null) //daca exista utilizatorul
            {
                var resultat = await logareManager.CheckPasswordSignInAsync(utilizator, logareUtilizatorModel.Parola, false);//false = nu il blochez
                if(resultat.Succeeded)
                {
                    var token = await tokenManager.CreareToken(utilizator);
                    return new TokenModel
                    { 
                        Token = token
                    };
                }
            }
            return null;
        }

        public async Task<IList<string>> Rol(LogareUtilizatorModel logareUtilizatorModel)
        {
            var utilizator = await utilizatorManager.FindByEmailAsync(logareUtilizatorModel.Email);
            if (utilizator != null)
            {
                var rol = await utilizatorManager.GetRolesAsync(utilizator);
                return rol;
            }
            return null;
        }
    }
}
