USE Vitae
GO

BEGIN TRY
    BEGIN TRANSACTION

-- Country
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'AF', 'Afghanistan', 'Afghanistan', 'Afghanistan', 'Afganist�n', 'Afghanistan', 'AFG', 4, 93)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'AL', 'Albanien', 'Albanie', 'Albania', 'Albania', 'Albania', 'ALB', 8, 355)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'DZ', 'Algerien', 'Alg�rie', 'Algeria', 'Argelia', 'Algeria', 'DZA', 12, 213)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'AS', 'Amerikanisch-Samoa', 'Les Samoa am�ricaines', 'Samoa americane', 'Samoa Americana', 'American Samoa', 'ASM', 16, 1684)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'AD', 'Andorra', 'Andorre', 'Andorra', 'Andorra', 'Andorra', 'AND', 20, 376)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'AO', 'Angola', 'Angola', 'Angola', 'Angola', 'Angola', 'AGO', 24, 244)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'AI', 'Anguilla', 'Anguilla', 'Anguilla', 'Anguila', 'Anguilla', 'AIA', 660, 1264)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'AQ', 'Antarktis', 'Antarctique', 'Antartide', 'Ant�rtida', 'Antarctica', 'ATA', NULL, 672)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'AG', 'Antigua und Barbuda', 'Antigua-et-Barbuda', 'Antigua e Barbuda', 'Antigua y Barbuda', 'Antigua and Barbuda', 'ATG', 28, 1268)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'AR', 'Argentinien', 'Argentine', 'Argentina', 'Argentina', 'Argentina', 'ARG', 32, 54)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'AM', 'Armenien', 'Arm�nie', 'Armenia', 'Armenia', 'Armenia', 'ARM', 51, 374)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'AW', 'Aruba', 'Aruba', 'Aruba', 'Aruba', 'Aruba', 'ABW', 533, 297)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'AU', 'Australien', 'Australie', 'Australia', 'Australia', 'Australia', 'AUS', 36, 61)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'AT', '�sterreich', 'Autriche', 'Austria', 'Austria', 'Austria', 'AUT', 40, 43)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'AZ', 'Aserbaidschan', 'Azerba�djan', 'Azerbaigian', 'Azerbaiy�n', 'Azerbaijan', 'AZE', 31, 994)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'BS', 'Bahamas', 'Bahamas', 'Bahamas', 'Bahamas', 'Bahamas', 'BHS', 44, 1242)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'BH', 'Bahrain', 'Bahre�n', 'Bahrain', 'Bahrein', 'Bahrain', 'BHR', 48, 973)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'BD', 'Bangladesch', 'Bangladesh', 'Bangladesh', 'Bangladesh', 'Bangladesh', 'BGD', 50, 880)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'BB', 'Barbados', 'Barbade', 'Barbados', 'Barbados', 'Barbados', 'BRB', 52, 1246)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'BY', 'Wei�russland', 'Bi�lorussie', 'Bielorussia', 'Bielorrusia', 'Belarus', 'BLR', 112, 375)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'BE', 'Belgien', 'Belgique', 'Belgio', 'B�lgica', 'Belgium', 'BEL', 56, 32)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'BZ', 'Belize', 'B�lize', 'Belize', 'Belice', 'Belize', 'BLZ', 84, 501)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'BJ', 'Benin', 'B�nin', 'Benin', 'Ben�n', 'Benin', 'BEN', 204, 229)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'BM', 'Bermuda', 'Bermudes', 'Bermuda', 'Bermudas', 'Bermuda', 'BMU', 60, 1441)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'BT', 'Bhutan', 'Bhoutan', 'Bhutan', 'Bhut�n', 'Bhutan', 'BTN', 64, 975)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'BO', 'Bolivien', 'Bolivie', 'Bolivia', 'Bolivia', 'Bolivia', 'BOL', 68, 591)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'BA', 'Bosnien und Herzegowina', 'Bosnie-Herz�govine', 'Bosnia ed Erzegovina', 'Bosnia y Herzegovina', 'Bosnia and Herzegovina', 'BIH', 70, 387)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'BW', 'Botswana', 'Botswana', 'Botswana', 'Botswana', 'Botswana', 'BWA', 72, 267)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'BV', 'Bouvet-Insel', 'L''�le Bouvet', 'Isola di Bouvet', 'isla Bouvet', 'Bouvet Island', 'BVT', NULL, 47)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'BR', 'Brasilien', 'Br�sil', 'Brasile', 'Brasil', 'Brazil', 'BRA', 76, 55)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'IO', 'Britisches Territorium im Indischen Ozean', 'Territoire britannique de l''oc�an Indien', 'Territorio britannico dell''Oceano Indiano', 'Territorio Brit�nico del Oc�ano �ndico', 'British Indian Ocean Territory', 'IOT', NULL, 246)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'BN', 'Brunei Darussalam', 'Brunei Darussalam', 'Brunei Darussalam', 'Brunei Darussalam', 'Brunei Darussalam', 'BRN', 96, 673)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'BG', 'Bulgarien', 'Bulgarie', 'Bulgaria', 'Bulgaria', 'Bulgaria', 'BGR', 100, 359)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'BF', 'Burkina Faso', 'Burkina Faso', 'Burkina Faso', 'Burkina Faso', 'Burkina Faso', 'BFA', 854, 226)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'BI', 'Burundi', 'Burundi', 'Burundi', 'Burundi', 'Burundi', 'BDI', 108, 257)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'KH', 'Kambodscha', 'Cambodge', 'Cambogia', 'Camboya', 'Cambodia', 'KHM', 116, 855)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'CM', 'Kamerun', 'Cameroun', 'Camerun', 'Camer�n', 'Cameroon', 'CMR', 120, 237)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'CA', 'Kanada', 'Canada', 'Canada', 'Canad�', 'Canada', 'CAN', 124, 1)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'CV', 'Kap Verde', 'Cap-Vert', 'Capo Verde', 'Cabo Verde', 'Cape Verde', 'CPV', 132, 238)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'KY', 'Kaimaninseln', '�les Ca�mans', 'Isole Cayman', 'Islas Caim�n', 'Cayman Islands', 'CYM', 136, 1345)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'CF', 'Zentralafrikanische Republik', 'R�publique centrafricaine', 'Repubblica Centrafricana', 'Rep�blica Centroafricana', 'Central African Republic', 'CAF', 140, 236)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'TD', 'Tschad', 'Tchad', 'Ciad', 'Chad', 'Chad', 'TCD', 148, 235)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'CL', 'Chile', 'Chili', 'Cile', 'Chile', 'Chile', 'CHL', 152, 56)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'CN', 'China', 'Chine', 'Cina', 'China', 'China', 'CHN', 156, 86)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'CX', 'Weihnachtsinsel', 'L''�le Christmas', 'Isola di Natale', 'Isla de Navidad', 'Christmas Island', 'CXR', NULL, 61)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'CC', 'Cocos-Inseln (kielend)', '�les Cocos (keeling)', 'Isole Cocos (Keeling)', 'Islas Cocos (quilla)', 'Cocos (Keeling) Islands', 'CCK', NULL, 672)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'CO', 'Kolumbien', 'Colombie', 'Colombia', 'Colombia', 'Colombia', 'COL', 170, 57)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'KM', 'Komoren', 'Comores', 'Comore', 'Comoras', 'Comoros', 'COM', 174, 269)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'CG', 'Kongo', 'Congo', 'Congo', 'Congo', 'Congo', 'COG', 178, 242)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'CD', 'Kongo, Die Demokratische Republik Kongo', 'Congo, R�publique d�mocratique du', 'Congo, Repubblica Democratica del', 'Congo, Rep�blica Democr�tica del', 'Congo, the Democratic Republic of the', 'COD', 180, 243)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'CK', 'Cook-Inseln', '�les Cook', 'Isole Cook', 'Islas Cook', 'Cook Islands', 'COK', 184, 682)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'CR', 'Costa Rica', 'Costa Rica', 'Costa Rica', 'Costa Rica', 'Costa Rica', 'CRI', 188, 506)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'CI', 'C�te d''ivoire', 'C�te D''ivoire', 'Costa D''Avorio', 'Costa de Marfil', 'Cote D''Ivoire', 'CIV', 384, 225)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'HR', 'Kroatien', 'Croatie', 'Croazia', 'Croacia', 'Croatia', 'HRV', 191, 385)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'CU', 'Kuba', 'Cuba', 'Cuba', 'Cuba', 'Cuba', 'CUB', 192, 53)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'CY', 'Zypern', 'Chypre', 'Cipro', 'Chipre', 'Cyprus', 'CYP', 196, 357)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'CZ', 'Tschechische Republik', 'R�publique tch�que', 'Repubblica Ceca', 'Rep�blica Checa', 'Czech Republic', 'CZE', 203, 420)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'DK', 'D�nemark', 'Danemark', 'Danimarca', 'Dinamarca', 'Denmark', 'DNK', 208, 45)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'DJ', 'Dschibuti', 'Djibouti', 'Gibuti', 'Yibuti', 'Djibouti', 'DJI', 262, 253)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'DM', 'Dominica', 'Dominique', 'Dominica', 'Dominica', 'Dominica', 'DMA', 212, 1767)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'DO', 'Dominikanische Republik', 'R�publique dominicaine', 'Repubblica Dominicana', 'Rep�blica Dominicana', 'Dominican Republic', 'DOM', 214, 1809)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'EC', 'Ecuador', '�quateur', 'Ecuador', 'Ecuador', 'Ecuador', 'ECU', 218, 593)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'EG', '�gypten', '�gypte', 'Egitto', 'Egipto', 'Egypt', 'EGY', 818, 20)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'SV', 'El Salvador', 'Le Salvador', 'El Salvador', 'El Salvador', 'El Salvador', 'SLV', 222, 503)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'GQ', '�quatorialguinea', 'Guin�e �quatoriale', 'Guinea Equatoriale', 'Guinea Ecuatorial', 'Equatorial Guinea', 'GNQ', 226, 240)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'ER', 'Eritrea', '�rythr�e', 'Eritrea', 'Eritrea', 'Eritrea', 'ERI', 232, 291)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'EE', 'Estland', 'Estonie', 'Estonia', 'Estonia', 'Estonia', 'EST', 233, 372)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'ET', '�thiopien', '�thiopie', 'Etiopia', 'Etiop�a', 'Ethiopia', 'ETH', 231, 251)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'FK', 'Falkland-Inseln (Malwinen)', '�les Malouines (malvinas)', 'Isole Falkland (malvinas)', 'Islas Malvinas (Falkland Islands)', 'Falkland Islands (Malvinas)', 'FLK', 238, 500)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'FO', 'F�r�er-Inseln', '�les F�ro�', 'Isole Faroe', 'Islas Feroe', 'Faroe Islands', 'FRO', 234, 298)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'FJ', 'Fidschi', 'Fidji', 'Figi', 'Fiji', 'Fiji', 'FJI', 242, 679)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'FI', 'Finnland', 'Finlande', 'Finlandia', 'Finlandia', 'Finland', 'FIN', 246, 358)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'FR', 'Frankreich', 'France', 'Francia', 'Francia', 'France', 'FRA', 250, 33)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'GF', 'Franz�sisch-Guayana', 'La Guyane fran�aise', 'Guyana Francese', 'Guayana Francesa', 'French Guiana', 'GUF', 254, 594)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'PF', 'Franz�sisch-Polynesien', 'Polyn�sie fran�aise', 'Polinesia Francese', 'Polinesia Francesa', 'French Polynesia', 'PYF', 258, 689)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'TF', 'Franz�sische S�dpolar-Territorien', 'Territoires fran�ais du Sud', 'Territori francesi del Sud', 'Territorios Franceses del Sur', 'French Southern Territories', 'ATF', NULL, 262)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'GA', 'Gabun', 'Gabon', 'Gabon', 'Gab�n', 'Gabon', 'GAB', 266, 241)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'GM', 'Gambia', 'Gambie', 'Gambia', 'Gambia', 'Gambia', 'GMB', 270, 220)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'GE', 'Georgien', 'G�orgie', 'Georgia', 'Georgia', 'Georgia', 'GEO', 268, 995)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'DE', 'Deutschland', 'Allemagne', 'Germania', 'Alemania', 'Germany', 'DEU', 276, 49)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'GH', 'Ghana', 'Ghana', 'Ghana', 'Ghana', 'Ghana', 'GHA', 288, 233)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'GI', 'Gibraltar', 'Gibraltar', 'Gibilterra', 'Gibraltar', 'Gibraltar', 'GIB', 292, 350)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'GR', 'Griechenland', 'Gr�ce', 'Grecia', 'Grecia', 'Greece', 'GRC', 300, 30)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'GL', 'Gr�nland', 'Groenland', 'Groenlandia', 'Groenlandia', 'Greenland', 'GRL', 304, 299)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'GD', 'Grenada', 'Grenade', 'Grenada', 'Granada', 'Grenada', 'GRD', 308, 1473)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'GP', 'Guadeloupe', 'Guadeloupe', 'Guadalupa', 'Guadalupe', 'Guadeloupe', 'GLP', 312, 590)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'GU', 'Guam', 'Guam', 'Guam', 'Guam', 'Guam', 'GUM', 316, 1671)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'GT', 'Guatemala', 'Guatemala', 'Guatemala', 'Guatemala', 'Guatemala', 'GTM', 320, 502)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'GN', 'Guinea', 'Guin�e', 'Guinea', 'Guinea', 'Guinea', 'GIN', 324, 224)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'GW', 'Guinea-Bissau', 'Guin�e-Bissau', 'Guinea-Bissau', 'Guinea-bissau', 'Guinea-Bissau', 'GNB', 624, 245)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'GY', 'Guyana', 'Guyane', 'Guyana', 'Guyana', 'Guyana', 'GUY', 328, 592)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'HT', 'Haiti', 'Ha�ti', 'Haiti', 'Hait�', 'Haiti', 'HTI', 332, 509)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'HM', 'Heard Island und Mcdonaldinseln', 'L''�le Heard et les �les Mcdonald', 'Isola Heard e Isole Mcdonald', 'Isla Heard e Islas Mcdonald', 'Heard Island and Mcdonald Islands', 'HMD', NULL, 0)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'VA', 'Heiliger Stuhl (Staat Vatikanstadt)', 'Saint-Si�ge (ville-�tat du Vatican)', 'Santa Sede (Stato della Citt� del Vaticano)', 'Santa Sede (Ciudad Estado del Vaticano)', 'Holy See (Vatican City State)', 'VAT', 336, 39)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'HN', 'Honduras', 'Honduras', 'Honduras', 'Honduras', 'Honduras', 'HND', 340, 504)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'HK', 'Hongkong', 'Hong Kong', 'Hong Kong', 'Hong Kong', 'Hong Kong', 'HKG', 344, 852)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'HU', 'Ungarn', 'Hongrie', 'Ungheria', 'Hungr�a', 'Hungary', 'HUN', 348, 36)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'IS', 'Island', 'Islande', 'Islanda', 'Islandia', 'Iceland', 'ISL', 352, 354)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'IN', 'Indien', 'Inde', 'India', 'India', 'India', 'IND', 356, 91)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'ID', 'Indonesien', 'Indon�sie', 'Indonesia', 'Indonesia', 'Indonesia', 'IDN', 360, 62)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'IR', 'Iran, Islamische Republik', 'Iran, R�publique islamique d', 'Iran, Repubblica Islamica di', 'Ir�n, Rep�blica Isl�mica del', 'Iran, Islamic Republic of', 'IRN', 364, 98)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'IQ', 'Irak', 'Irak', 'Iraq', 'Iraq', 'Iraq', 'IRQ', 368, 964)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'IE', 'Irland', 'Irlande', 'Irlanda', 'Irlanda', 'Ireland', 'IRL', 372, 353)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'IL', 'Israel', 'Isra�l', 'Israele', 'Israel', 'Israel', 'ISR', 376, 972)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'IT', 'Italien', 'Italie', 'Italia', 'Italia', 'Italy', 'ITA', 380, 39)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'JM', 'Jamaika', 'Jama�que', 'Giamaica', 'Jamaica', 'Jamaica', 'JAM', 388, 1876)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'JP', 'Japan', 'Japon', 'Giappone', 'Jap�n', 'Japan', 'JPN', 392, 81)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'JO', 'Jordanien', 'Jordanie', 'Giordania', 'Jordania', 'Jordan', 'JOR', 400, 962)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'KZ', 'Kasachstan', 'Kazakhstan', 'Kazakistan', 'Kazajst�n', 'Kazakhstan', 'KAZ', 398, 7)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'KE', 'Kenia', 'Kenya', 'Kenya', 'Kenia', 'Kenya', 'KEN', 404, 254)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'KI', 'Kiribati', 'Kiribati', 'Kiribati', 'Kiribati', 'Kiribati', 'KIR', 296, 686)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'KP', 'Korea, Demokratische Volksrepublik', 'Cor�e, R�publique populaire d�mocratique de', 'Corea, Repubblica Democratica Popolare di', 'Corea, Rep�blica Popular Democr�tica de', 'Korea, Democratic People''s Republic of', 'PRK', 408, 850)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'KR', 'Korea, Republik', 'Cor�e, R�publique de', 'Corea, Repubblica di', 'Corea, Rep�blica de', 'Korea, Republic of', 'KOR', 410, 82)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'KW', 'Kuwait', 'Kowe�t', 'Kuwait', 'Kuwait', 'Kuwait', 'KWT', 414, 965)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'KG', 'Kirgisistan', 'Kirghizistan', 'Kirghizistan', 'Kirguist�n', 'Kyrgyzstan', 'KGZ', 417, 996)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'LA', 'Demokratische Volksrepublik Laos', 'R�publique d�mocratique populaire lao', 'Repubblica Democratica Popolare del Laos', 'Rep�blica Democr�tica Popular Lao', 'Lao People''s Democratic Republic', 'LAO', 418, 856)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'LV', 'Lettland', 'Lettonie', 'Lettonia', 'Letonia', 'Latvia', 'LVA', 428, 371)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'LB', 'Libanon', 'Liban', 'Libano', 'L�bano', 'Lebanon', 'LBN', 422, 961)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'LS', 'Lesotho', 'Lesotho', 'Lesotho', 'Lesotho', 'Lesotho', 'LSO', 426, 266)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'LR', 'Liberia', 'Liberia', 'Liberia', 'Liberia', 'Liberia', 'LBR', 430, 231)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'LY', 'Libysch-Arabische Dschamahirija', 'Jamahiriya arabe libyenne', 'Jamahiriya araba libica', 'Jamahiriya �rabe Libia', 'Libyan Arab Jamahiriya', 'LBY', 434, 218)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'LI', 'Liechtenstein', 'Liechtenstein', 'Liechtenstein', 'Liechtenstein', 'Liechtenstein', 'LIE', 438, 423)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'LT', 'Litauen', 'Lituanie', 'Lituania', 'Lituania', 'Lithuania', 'LTU', 440, 370)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'LU', 'Luxemburg', 'Luxembourg', 'Lussemburgo', 'Luxemburgo', 'Luxembourg', 'LUX', 442, 352)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'MO', 'Macao', 'Macao', 'Macao', 'Macao', 'Macao', 'MAC', 446, 853)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'MK', 'Mazedonien, Ehemalige Jugoslawische Republik', 'Mac�doine, l''ex-R�publique yougoslave de', 'Macedonia, Ex Repubblica Jugoslava di', 'Macedonia, la ex Rep�blica Yugoslava de', 'Macedonia, the Former Yugoslav Republic of', 'MKD', 807, 389)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'MG', 'Madagaskar', 'Madagascar', 'Madagascar', 'Madagascar', 'Madagascar', 'MDG', 450, 261)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'MW', 'Malawi', 'Malawi', 'Malawi', 'Malawi', 'Malawi', 'MWI', 454, 265)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'MY', 'Malaysia', 'Malaisie', 'Malesia', 'Malasia', 'Malaysia', 'MYS', 458, 60)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'MV', 'Malediven', 'Maldives', 'Maldive', 'Maldivas', 'Maldives', 'MDV', 462, 960)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'ML', 'Mali', 'Mali', 'Mali', 'Mal�', 'Mali', 'MLI', 466, 223)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'MT', 'Malta', 'Malte', 'Malta', 'Malta', 'Malta', 'MLT', 470, 356)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'MH', 'Marshall-Inseln', '�les Marshall', 'Isole Marshall', 'Islas Marshall', 'Marshall Islands', 'MHL', 584, 692)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'MQ', 'Martinique', 'Martinique', 'Martinica', 'Martinica', 'Martinique', 'MTQ', 474, 596)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'MR', 'Mauretanien', 'Mauritanie', 'Mauritania', 'Mauritania', 'Mauritania', 'MRT', 478, 222)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'MU', 'Mauritius', 'Maurice', 'Mauritius', 'Mauricio', 'Mauritius', 'MUS', 480, 230)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'YT', 'Mayotte', 'Mayotte', 'Mayotte', 'Mayotte', 'Mayotte', 'MYT', NULL, 269)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'MX', 'Mexiko', 'Mexique', 'Messico', 'M�xico', 'Mexico', 'MEX', 484, 52)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'FM', 'Mikronesien, F�derierte Staaten von', 'Micron�sie, �tats f�d�r�s de', 'Micronesia, Stati Federati di', 'Micronesia, Estados Federados de', 'Micronesia, Federated States of', 'FSM', 583, 691)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'MD', 'Moldawien, Republik', 'Moldavie, R�publique de', 'Moldavia, Repubblica di', 'Moldavia, Rep�blica de', 'Moldova, Republic of', 'MDA', 498, 373)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'MC', 'Monaco', 'Monaco', 'Monaco', 'M�naco', 'Monaco', 'MCO', 492, 377)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'MN', 'Mongolei', 'Mongolie', 'Mongolia', 'Mongolia', 'Mongolia', 'MNG', 496, 976)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'MS', 'Montserrat', 'Montserrat', 'Montserrat', 'Montserrat', 'Montserrat', 'MSR', 500, 1664)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'MA', 'Marokko', 'Maroc', 'Marocco', 'Marruecos', 'Morocco', 'MAR', 504, 212)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'MZ', 'Mosambik', 'Mozambique', 'Mozambico', 'Mozambique', 'Mozambique', 'MOZ', 508, 258)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'MM', 'Myanmar', 'Myanmar', 'Myanmar', 'Myanmar', 'Myanmar', 'MMR', 104, 95)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'NA', 'Namibia', 'Namibie', 'Namibia', 'Namibia', 'Namibia', 'NAM', 516, 264)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'NR', 'Nauru', 'Nauru', 'Nauru', 'Nauru', 'Nauru', 'NRU', 520, 674)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'NP', 'Nepal', 'N�pal', 'Nepal', 'Nepal', 'Nepal', 'NPL', 524, 977)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'NL', 'Niederlande', 'Pays-Bas', 'Paesi Bassi', 'Pa�ses Bajos', 'Netherlands', 'NLD', 528, 31)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'AN', 'Niederl�ndische Antillen', 'Antilles n�erlandaises', 'Antille olandesi', 'Antillas Holandesas', 'Netherlands Antilles', 'ANT', 530, 599)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'NC', 'Neukaledonien', 'Nouvelle-Cal�donie', 'Nuova Caledonia', 'Nueva Caledonia', 'New Caledonia', 'NCL', 540, 687)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'NZ', 'Neuseeland', 'Nouvelle-Z�lande', 'Nuova Zelanda', 'Nueva Zelanda', 'New Zealand', 'NZL', 554, 64)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'NI', 'Nicaragua', 'Nicaragua', 'Nicaragua', 'Nicaragua', 'Nicaragua', 'NIC', 558, 505)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'NE', 'Niger', 'Niger', 'Niger', 'N�ger', 'Niger', 'NER', 562, 227)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'NG', 'Nigeria', 'Nigeria', 'Nigeria', 'Nigeria', 'Nigeria', 'NGA', 566, 234)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'NU', 'Niue', 'Niue', 'Niue', 'Niue', 'Niue', 'NIU', 570, 683)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'NF', 'Norfolkinsel', 'L''�le de Norfolk', 'Isola di Norfolk', 'Isla de Norfolk', 'Norfolk Island', 'NFK', 574, 672)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'MP', 'N�rdliche Marianen-Inseln', '�les Mariannes du Nord', 'Isole Marianne Settentrionali', 'Islas Marianas del Norte', 'Northern Mariana Islands', 'MNP', 580, 1670)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'NO', 'Norwegen', 'Norv�ge', 'Norvegia', 'Noruega', 'Norway', 'NOR', 578, 47)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'OM', 'Oman', 'Oman', 'Oman', 'Om�n', 'Oman', 'OMN', 512, 968)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'PK', 'Pakistan', 'Pakistan', 'Pakistan', 'Pakist�n', 'Pakistan', 'PAK', 586, 92)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'PW', 'Palau', 'Palau', 'Palau', 'Palaos', 'Palau', 'PLW', 585, 680)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'PS', 'Pal�stinensisches Gebiet, besetzte Gebiete', 'Territoire palestinien occup�', 'Territorio Palestinese, Occupato', 'Territorio Palestino Ocupado', 'Palestinian Territory, Occupied', 'PSE', NULL, 970)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'PA', 'Panama', 'Panama', 'Panama', 'Panam�', 'Panama', 'PAN', 591, 507)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'PG', 'Papua-Neuguinea', 'Papouasie-Nouvelle-Guin�e', 'Papua Nuova Guinea', 'Pap�a Nueva Guinea', 'Papua New Guinea', 'PNG', 598, 675)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'PY', 'Paraguay', 'Paraguay', 'Paraguay', 'Paraguay', 'Paraguay', 'PRY', 600, 595)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'PE', 'Peru', 'P�rou', 'Per�', 'Per�', 'Peru', 'PER', 604, 51)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'PH', 'Philippinen', 'Philippines', 'Filippine', 'Filipinas', 'Philippines', 'PHL', 608, 63)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'PN', 'Pitcairn', 'Pitcairn', 'Pitcairn', 'Pitcairn', 'Pitcairn', 'PCN', 612, 64)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'PL', 'Polen', 'Pologne', 'Polonia', 'Polonia', 'Poland', 'POL', 616, 48)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'PT', 'Portugal', 'Portugal', 'Portogallo', 'Portugal', 'Portugal', 'PRT', 620, 351)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'PR', 'Puerto Rico', 'Porto Rico', 'Porto Rico', 'Puerto Rico', 'Puerto Rico', 'PRI', 630, 1787)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'QA', 'Katar', 'Qatar', 'Qatar', 'Qatar', 'Qatar', 'QAT', 634, 974)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'RE', 'Wiedervereinigung', 'R�union', 'Riunione', 'Reuni�n', 'Reunion', 'REU', 638, 262)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'RO', 'Rum�nien', 'Roumanie', 'Romania', 'Rumania', 'Romania', 'ROU', 642, 40)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'RU', 'Russische F�deration', 'F�d�ration de Russie', 'Federazione Russa', 'Federaci�n de Rusia', 'Russian Federation', 'RUS', 643, 7)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'RW', 'Ruanda', 'Rwanda', 'Ruanda', 'Ruanda', 'Rwanda', 'RWA', 646, 250)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'SH', 'Heilige Helena', 'Sainte-H�l�ne', 'Sant''Elena', 'Santa Elena', 'Saint Helena', 'SHN', 654, 290)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'KN', 'St. Kitts und Nevis', 'Saint-Kitts-et-Nevis', 'Santi Gattini e Nevis', 'San Crist�bal y Nieves', 'Saint Kitts and Nevis', 'KNA', 659, 1869)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'LC', 'St. Lucia', 'Sainte-Lucie', 'Santa Lucia', 'Santa Luc�a', 'Saint Lucia', 'LCA', 662, 1758)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'PM', 'St. Pierre und Miquelon', 'Saint-Pierre-et-Miquelon', 'San Pietro e Miquelon', 'San Pedro y Miquel�n', 'Saint Pierre and Miquelon', 'SPM', 666, 508)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'VC', 'St. Vinzenz und die Grenadinen', 'Saint Vincent et les Grenadines', 'San Vincenzo e le Grenadine', 'San Vicente y las Granadinas', 'Saint Vincent and the Grenadines', 'VCT', 670, 1784)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'WS', 'Samoa', 'Samoa', 'Samoa', 'Samoa', 'Samoa', 'WSM', 882, 684)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'SM', 'San Marino', 'Saint-Marin', 'San Marino', 'San Marino', 'San Marino', 'SMR', 674, 378)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'ST', 'S�o Tom� und Pr�ncipe', 'Sao Tom�-et-Principe', 'Sao Tome e Principe', 'Santo Tom� y Pr�ncipe', 'Sao Tome and Principe', 'STP', 678, 239)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'SA', 'Saudi-Arabien', 'Arabie Saoudite', 'Arabia Saudita', 'Arabia Saudita', 'Saudi Arabia', 'SAU', 682, 966)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'SN', 'Senegal', 'S�n�gal', 'Senegal', 'Senegal', 'Senegal', 'SEN', 686, 221)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'SC', 'Seychellen', 'Seychelles', 'Seychelles', 'Seychelles', 'Seychelles', 'SYC', 690, 248)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'SL', 'Sierra Leone', 'Sierra Leone', 'Sierra Leone', 'Sierra Leona', 'Sierra Leone', 'SLE', 694, 232)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'SG', 'Singapur', 'Singapour', 'Singapore', 'Singapur', 'Singapore', 'SGP', 702, 65)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'SK', 'Slowakei', 'Slovaquie', 'Slovacchia', 'Eslovaquia', 'Slovakia', 'SVK', 703, 421)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'SI', 'Slowenien', 'Slov�nie', 'Slovenia', 'Eslovenia', 'Slovenia', 'SVN', 705, 386)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'SB', 'Salomon-Inseln', '�les Salomon', 'Isole Salomone', 'Islas Salom�n', 'Solomon Islands', 'SLB', 90, 677)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'SO', 'Somalia', 'Somalie', 'Somalia', 'Somalia', 'Somalia', 'SOM', 706, 252)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'ZA', 'S�dafrika', 'Afrique du Sud', 'Sudafrica', 'Sud�frica', 'South Africa', 'ZAF', 710, 27)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'GS', 'S�dgeorgien und die s�dlichen Sandwichinseln', 'G�orgie du Sud et les �les Sandwich du Sud', 'Georgia del Sud e Isole Sandwich del Sud', 'Georgia del Sur y las Islas Sandwich del Sur', 'South Georgia and the South Sandwich Islands', 'SGS', NULL, 500)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'ES', 'Spanien', 'Espagne', 'Spagna', 'Espa�a', 'Spain', 'ESP', 724, 34)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'LK', 'Sri Lanka', 'Sri Lanka', 'Sri Lanka', 'Sri Lanka', 'Sri Lanka', 'LKA', 144, 94)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'SD', 'Sudan', 'Soudan', 'Sudan', 'Sud�n', 'Sudan', 'SDN', 736, 249)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'SR', 'Surinam', 'Suriname', 'Suriname', 'Surinam', 'Suriname', 'SUR', 740, 597)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'SJ', 'Svalbard und Jan Mayen', 'Svalbard et Jan Mayen', 'Svalbard e Jan Mayen', 'Svalbard y Jan Mayen', 'Svalbard and Jan Mayen', 'SJM', 744, 47)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'SZ', 'Swasiland', 'Swaziland', 'Swaziland', 'Suazilandia', 'Swaziland', 'SWZ', 748, 268)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'SE', 'Schweden', 'Su�de', 'Svezia', 'Suecia', 'Sweden', 'SWE', 752, 46)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'CH', 'Schweiz', 'Suisse', 'Svizzera', 'Suiza', 'Switzerland', 'CHE', 756, 41)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'SY', 'Arabische Republik Syrien', 'R�publique arabe syrienne', 'Repubblica Araba Siriana', 'Rep�blica �rabe Siria', 'Syrian Arab Republic', 'SYR', 760, 963)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'TW', 'Taiwan, Provinz von China', 'Taiwan, Province de Chine', 'Taiwan, Provincia della Cina', 'Taiw�n, Provincia de China', 'Taiwan, Province of China', 'TWN', 158, 886)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'TJ', 'Tadschikistan', 'Tadjikistan', 'Tajikistan', 'Tayikist�n', 'Tajikistan', 'TJK', 762, 992)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'TZ', 'Tansania, Vereinigte Republik', 'Tanzanie, R�publique-Unie de', 'Tanzania, Repubblica Unita di', 'Tanzania, Rep�blica Unida de', 'Tanzania, United Republic of', 'TZA', 834, 255)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'TH', 'Thailand', 'Tha�lande', 'Thailandia', 'Tailandia', 'Thailand', 'THA', 764, 66)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'TL', 'Timor-Leste', 'Timor-Leste', 'Timor-leste', 'Timor Oriental', 'Timor-Leste', 'TLS', NULL, 670)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'TG', 'Togo', 'Togo', 'Togo', 'Togo', 'Togo', 'TGO', 768, 228)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'TK', 'Tokelau', 'Tokelau', 'Tokelau', 'Tokelau', 'Tokelau', 'TKL', 772, 690)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'TO', 'Tonga', 'Tonga', 'Tonga', 'Tonga', 'Tonga', 'TON', 776, 676)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'TT', 'Trinidad und Tobago', 'Trinit�-et-Tobago', 'Trinidad e Tobago', 'Trinidad y Tobago', 'Trinidad and Tobago', 'TTO', 780, 1868)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'TN', 'Tunesien', 'Tunisie', 'Tunisia', 'T�nez', 'Tunisia', 'TUN', 788, 216)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'TR', 'T�rkei', 'Turquie', 'Turchia', 'Turqu�a', 'Turkey', 'TUR', 792, 90)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'TM', 'Turkmenistan', 'Turkm�nistan', 'Turkmenistan', 'Turkmenist�n', 'Turkmenistan', 'TKM', 795, 7370)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'TC', 'T�rken und Caicos-Inseln', '�les Turks et Ca�ques', 'Isole Turks e Caicos', 'Islas Turcas y Caicos', 'Turks and Caicos Islands', 'TCA', 796, 1649)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'TV', 'Tuvalu', 'Tuvalu', 'Tuvalu', 'Tuvalu', 'Tuvalu', 'TUV', 798, 688)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'UG', 'Uganda', 'Ouganda', 'Uganda', 'Uganda', 'Uganda', 'UGA', 800, 256)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'UA', 'Ukraine', 'Ukraine', 'Ucraina', 'Ucrania', 'Ukraine', 'UKR', 804, 380)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'AE', 'Vereinigte Arabische Emirate', '�mirats arabes unis', 'Emirati Arabi Uniti', 'Emiratos �rabes Unidos', 'United Arab Emirates', 'ARE', 784, 971)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'GB', 'Vereinigtes K�nigreich', 'Royaume-Uni', 'Regno Unito', 'Reino Unido', 'United Kingdom', 'GBR', 826, 44)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'US', 'Vereinigte Staaten', '�tats-Unis', 'Stati Uniti', 'Estados Unidos', 'United States', 'USA', 840, 1)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'UM', 'Kleinere Inseln der Vereinigten Staaten', '�les mineures �loign�es des �tats-Unis', 'Isole Minori lontane degli Stati Uniti', 'Islas menores de Estados Unidos', 'United States Minor Outlying Islands', 'UMI', NULL, 1)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'UY', 'Uruguay', 'Uruguay', 'Uruguay', 'Uruguay', 'Uruguay', 'URY', 858, 598)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'UZ', 'Usbekistan', 'Ouzb�kistan', 'Uzbekistan', 'Uzbekist�n', 'Uzbekistan', 'UZB', 860, 998)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'VU', 'Vanuatu', 'Vanuatu', 'Vanuatu', 'Vanuatu', 'Vanuatu', 'VUT', 548, 678)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'VE', 'Venezuela', 'Venezuela', 'Venezuela', 'Venezuela', 'Venezuela', 'VEN', 862, 58)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'VN', 'Vietnam', 'Le Viet Nam', 'Vietnam', 'Viet Nam', 'Viet Nam', 'VNM', 704, 84)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'VG', 'Jungferninseln, Britisch', '�les Vierges britanniques', 'Isole Vergini britanniche', 'Islas V�rgenes Brit�nicas', 'Virgin Islands, British', 'VGB', 92, 1284)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'VI', 'Jungferninseln, USA.', '�les Vierges, �tats-Unis', 'Isole Vergini, Stati Uniti', 'Islas V�rgenes, EE.UU.', 'Virgin Islands, U.s.', 'VIR', 850, 1340)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'WF', 'Wallis und Futuna', 'Wallis et Futuna', 'Wallis e Futuna', 'Wallis y Futuna', 'Wallis and Futuna', 'WLF', 876, 681)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'EH', 'Westsahara', 'Sahara occidental', 'Sahara occidentale', 'El Sahara Occidental', 'Western Sahara', 'ESH', 732, 212)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'YE', 'Jemen', 'Y�men', 'Yemen', 'Yemen', 'Yemen', 'YEM', 887, 967)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'ZM', 'Sambia', 'Zambie', 'Zambia', 'Zambia', 'Zambia', 'ZMB', 894, 260)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'ZW', 'Simbabwe', 'Zimbabwe', 'Zimbabwe', 'Zimbabwe', 'Zimbabwe', 'ZWE', 716, 263)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'RS', 'Serbien', 'Serbie', 'Serbia', 'Serbia', 'Serbia', 'SRB', 688, 381)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'AP', 'Asien-Pazifik-Region', 'R�gion Asie-Pacifique', 'Regione Asia Pacifico', 'Regi�n de Asia y el Pac�fico', 'Asia / Pacific Region', '0', 0, 0)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'ME', 'Montenegro', 'Mont�n�gro', 'Montenegro', 'Montenegro', 'Montenegro', 'MNE', 499, 382)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'AX', 'Aland-Inseln', '�les Aland', 'Isole Aland', 'Islas Aland', 'Aland Islands', 'ALA', 248, 358)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'BQ', 'Bonaire, Sint Eustatius und Saba', 'Bonaire, Saint-Eustache et Saba', 'Bonaire, Sint Eustatius e Saba', 'Bonaire, San Eustaquio y Saba', 'Bonaire, Sint Eustatius and Saba', 'BES', 535, 599)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'CW', 'Curacao', 'Cura�ao', 'Curacao', 'Cura�ao', 'Curacao', 'CUW', 531, 599)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'GG', 'Guernsey', 'Guernesey', 'Guernsey', 'Guernesey', 'Guernsey', 'GGY', 831, 44)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'IM', 'Insel Man', 'L''�le de Man', 'Isola di Man', 'Isla de Man', 'Isle of Man', 'IMN', 833, 44)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'JE', 'Jersey', 'Jersey', 'Jersey', 'Jersey', 'Jersey', 'JEY', 832, 44)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'XK', 'Kosovo', 'Kosovo', 'Kosovo', 'Kosovo', 'Kosovo', 'XKX', 0, 383)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'BL', 'Der Heilige Barthelemy', 'Saint-Barth�lemy', 'San Bartolomeo', 'San Bartolom�', 'Saint Barthelemy', 'BLM', 652, 590)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'MF', 'Sankt Martin', 'Saint Martin', 'San Martino', 'San Mart�n', 'Saint Martin', 'MAF', 663, 590)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'SX', 'Sint Maarten', 'Sint Maarten', 'Sint Maarten', 'San Mart�n', 'Sint Maarten', 'SXM', 534, 1)
INSERT INTO [Country] ([CountryID], [CountryCode], [Name_de], [Name_fr], [Name_it], [Name_es], [Name], [Iso3], [NumCode], [PhoneCode]) VALUES (NEWID(), 'SS', 'S�dsudan', 'Sud-Soudan', 'Sud Sudan', 'Sud�n del Sur', 'South Sudan', 'SSD', 728, 211)

-- Language
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'ab', 'Abkhazian', 'Abchasisch', 'Abkhazie', 'Abkhazia', 'Abjasia')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'aa', 'Afar', 'Afar', 'Afar', 'Afar', 'Afar')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'af', 'Afrikaans', 'Afrikaans', 'Afrikaans', 'Afrikaans', 'Afrik�ans')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'sq', 'Albanian', 'Albanisch', 'Albanais', 'Albanese', 'Alban�s')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'am', 'Amharic', 'Amharisch', 'Amharique', 'Amarico', 'Amh�rico')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'ar', 'Arabic', 'Arabisch', 'Arabe', 'Arabo', '�rabe')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'hy', 'Armenian', 'Armenisch', 'Arm�nien', 'Armeno', 'Armenio')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'as', 'Assamese', 'Assamese', 'Assamais', 'Assamese', 'Asam�s')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'ay', 'Aymara', 'Aymara', 'Aymara', 'Aymara', 'Aymara')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'az', 'Azerbaijani', 'Aserbaidschanisch', 'Azerba�djanais', 'Azerbaigian', 'Azerbaiy�n')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'ba', 'Bashkir', 'Baschkirisch', 'Bashkir', 'Bashkir', 'Bashkir')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'eu', 'Basque', 'Baskisch', 'Basque', 'Basco', 'Vasca')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'bn', 'Bengali, Bangla', 'Bengalisch, Bangla', 'Bengali, Bangla', 'Bengala, Bangla', 'Bengal�, Bangla')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'dz', 'Bhutani', 'Bhutani', 'Bhoutani', 'Bhutani', 'Bhutani')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'bh', 'Bihari', 'Bihari', 'Bihari', 'Bihari', 'Bihari')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'bi', 'Bislama', 'Bislama', 'Bislama', 'Bislama', 'Bislama')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'br', 'Breton', 'Bretonisch', 'Breton', 'Bretone', 'Bret�n')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'bg', 'Bulgarian', 'Bulgarisch', 'Bulgare', 'Bulgaro', 'B�lgaro')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'my', 'Burmese', 'Birmanisch', 'Birman', 'Birmani', 'Birmano')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'be', 'Byelorussian', 'Wei�russisch', 'Bi�lorusse', 'Bielorusso', 'Bielorrusia')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'km', 'Cambodian', 'Kambodschanisch', 'Cambodgien', 'Cambogiano', 'Camboyano')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'ca', 'Catalan', 'Katalanisch', 'En catalan', 'Catalano', 'Catal�n')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'zh', 'Chinese', 'Chinesisch', 'Chinois', 'Cinese', 'Chino')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'co', 'Corsican', 'Korsisch', 'Corse', 'Corso', 'Corso')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'hr', 'Croatian', 'Kroatisch', 'Croate', 'Croato', 'Croata')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'cs', 'Czech', 'Tschechisch', 'Tch�que', 'Ceco', 'Checo')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'da', 'Danish', 'D�nisch', 'Danois', 'Danese', 'Dan�s')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'nl', 'Dutch', 'Niederl�ndisch', 'N�erlandais', 'Olandese', 'Holand�s')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'en', 'English', 'Englisch', 'Anglais', 'Inglese', 'Ingl�s')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'eo', 'Esperanto', 'Esperanto', 'Esp�ranto', 'Esperanto', 'Esperanto')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'et', 'Estonian', 'Estnisch', 'Estonien', 'Estone', 'Estonio')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'fo', 'Faeroese', 'F�r�er', 'F�ro�en', 'Faeroese', 'Faeroese')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'fj', 'Fiji', 'Fidschi', 'Fidji', 'Figi', 'Fiji')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'fi', 'Finnish', 'Finnisch', 'En finnois', 'Finlandese', 'Finland�s')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'fr', 'French', 'Franz�sisch', 'Fran�ais', 'Francese', 'Franc�s')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'fy', 'Frisian', 'Friesisch', 'Frison', 'Frisona', 'Fris�n')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'gd', 'Gaelic (Scots Gaelic)', 'G�lisch (Schottisch-G�lisch)', 'Ga�lique (ga�lique �cossais)', 'Gaelico (Gaelico scozzese)', 'Ga�lico (Ga�lico Escoc�s)')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'gl', 'Galician', 'Galicisch', 'Galicien', 'Galiziano', 'Gallego')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'ka', 'Georgian', 'georgisch', 'G�orgien', 'Georgiano', 'Georgiano')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'de', 'German', 'Deutsch', 'En allemand', 'Tedesco', 'Alem�n')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'el', 'Greek', 'Griechisch', 'Grec', 'Greco', 'Griego')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'kl', 'Greenlandic', 'Gr�nl�ndisch', 'Groenlandais', 'Groenlandia', 'Groenlandia')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'gn', 'Guarani', 'Guarani', 'Guarani', 'Guarani', 'Guaran�')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'gu', 'Gujarati', 'Gujarati', 'Gujarati', 'Gujarati', 'Gujarati')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'ha', 'Hausa', 'Hausa', 'Hausa', 'Hausa', 'Hausa')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'iw', 'Hebrew', 'Hebr�isch', 'H�breu', 'Ebraico', 'Hebreo')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'hi', 'Hindi', 'Hindi', 'Hindi', 'Hindi', 'Hindi')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'hu', 'Hungarian', 'Ungarisch', 'Hongrois', 'Ungherese', 'H�ngaro')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'is', 'Icelandic', 'Isl�ndisch', 'Islandais', 'Islandese', 'Islandia')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'in', 'Indonesian', 'Indonesisch', 'Indon�sien', 'Indonesiano', 'Indonesio')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'ia', 'Interlingua', 'Interlingua', 'Interlingua', 'Interlingua', 'Interlingua')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'ie', 'Interlingue', 'Interlingue', 'Interlingue', 'Interlingue', 'Interlingue')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'ik', 'Inupiak', 'Inupiak', 'Inupiak', 'Inupiak', 'Inupiak')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'ga', 'Irish', 'Irisch', 'En irlandais', 'Irlandese', 'Irland�s')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'it', 'Italian', 'Italienisch', 'Italien', 'Italiano', 'Italiano')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'ja', 'Japanese', 'Japanisch', 'Japonais', 'Giapponese', 'Japon�s')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'jw', 'Javanese', 'Javanisch', 'Javanais', 'Giavanese', 'Javan�s')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'kn', 'Kannada', 'Kannada', 'Kannada', 'Kannada', 'Kannada')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'ks', 'Kashmiri', 'Kaschmir', 'Cachemire', 'Kashmiri', 'Cachemira')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'kk', 'Kazakh', 'Kasachisch', 'Kazakh', 'Kazakistan', 'Kazajst�n')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'rw', 'Kinyarwanda', 'Kinyarwanda', 'Kinyarwanda', 'Kinyarwanda', 'Kinyarwanda')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'ky', 'Kirghiz', 'Kirgisisch', 'Kirghiz', 'Kirghizistan', 'Kirgu�s')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'rn', 'Kirundi', 'Kirundi', 'Kirundi', 'Kirundi', 'Kirundi')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'ko', 'Korean', 'Koreanisch', 'Cor�en', 'Coreano', 'Coreano')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'ku', 'Kurdish', 'Kurdisch', 'Kurde', 'Curdo', 'Kurdo')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'lo', 'Laothian', 'Laothianisch', 'Laothian', 'Laothian', 'Laothian')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'la', 'Latin', 'Latein', 'Latin', 'Latino', 'Lat�n')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'lv', 'Latvian, Lettish', 'Lettisch, Lettisch', 'Letton, L�tarien', 'Lettone, Lettone', 'Let�n, Let�n')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'ln', 'Lingala', 'Lingala', 'Lingala', 'Lingala', 'Lingala')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'lt', 'Lithuanian', 'Litauisch', 'En lituanien', 'Lituano', 'Lituano')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'mk', 'Macedonian', 'Mazedonisch', 'Mac�donien', 'Macedone', 'Macedonio')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'mg', 'Malagasy', 'Madagassisch', 'Malgache', 'Malgascio', 'Malgache')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'ms', 'Malay', 'Malaysisch', 'Malais', 'Malese', 'Malayo')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'ml', 'Malayalam', 'Malayalam', 'Malayalam', 'Malayalam', 'Malayalam')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'mt', 'Maltese', 'Maltesisch', 'Maltais', 'Maltese', 'Malt�s')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'mi', 'Maori', 'Maori', 'Maori', 'Maori', 'Maor�')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'mr', 'Marathi', 'Marathi', 'Marathi', 'Marathi', 'Marathi')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'mo', 'Moldavian', 'Moldawisch', 'Moldavie', 'Moldava', 'Moldavia')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'mn', 'Mongolian', 'Mongolisch', 'Mongol', 'Mongolo', 'Mongol')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'na', 'Nauru', 'Nauru', 'Nauru', 'Nauru', 'Nauru')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'ne', 'Nepali', 'Nepali', 'N�palais', 'Nepali', 'Nepal�')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'no', 'Norwegian', 'Norwegisch', 'Norv�gien', 'Norvegese', 'Noruego')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'oc', 'Occitan', 'Okzitanisch', 'Occitan', 'Occitano', 'Occitano')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'or', 'Oriya', 'Oriya', 'Oriya', 'Oriya', 'Oriya')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'om', 'Oromo, Afan', 'Oromo, Afan', 'Oromo, Afan', 'Oromo, Afan', 'Oromo, Afan')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'ps', 'Pashto, Pushto', 'Paschtu, Paschtu', 'Pachtoune, Pachtoune', 'Pashto, Pushto', 'Pashto, Pushto')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'fa', 'Persian', 'Persisch', 'Persan', 'Persiano', 'Persa')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'pl', 'Polish', 'Polnisch', 'Polonais', 'Polacco', 'Polaco')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'pt', 'Portuguese', 'Portugiesisch', 'Portugais', 'Portoghese', 'Portugu�s')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'pa', 'Punjabi', 'Punjabi', 'Punjabi', 'Punjabi', 'Punjabi')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'qu', 'Quechua', 'Quechua', 'Quechua', 'Quechua', 'Quechua')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'rm', 'Rhaeto-Romance', 'R�toromanisch', 'Rhaeto-Romance', 'Rhaeto-Romance', 'Retorrom�nico')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'ro', 'Romanian', 'Rum�nisch', 'En roumain', 'Rumeno', 'Rumano')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'ru', 'Russian', 'Russisch', 'Russe', 'Russo', 'Ruso')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'sm', 'Samoan', 'Samoanisch', 'Samoan', 'Samoan', 'Samoano')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'sg', 'Sangro', 'Sangro', 'Sangro', 'Sangro', 'Sangro')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'sa', 'Sanskrit', 'Sanskrit', 'Sanskrit', 'Sanscrito', 'S�nscrito')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'sr', 'Serbian', 'Serbisch', 'Serbe', 'Serbo', 'Serbio')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'sh', 'Serbo-Croatian', 'Serbokroatisch', 'Serbo-croate', 'Serbo-croato', 'Serbocroata')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'st', 'Sesotho', 'Sesotho', 'Sesotho', 'Sesotho', 'Sesotho')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'tn', 'Setswana', 'Setswana', 'Setswana', 'Setswana', 'Setswana')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'sn', 'Shona', 'Shona', 'Shona', 'Shona', 'Shona')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'sd', 'Sindhi', 'Sindhi', 'Sindhi', 'Sindhi', 'Sindhi')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'si', 'Singhalese', 'Singhalesisch', 'Singhalais', 'Singhalese', 'Cingal�s')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'ss', 'Siswati', 'Siswati', 'Siswati', 'Siswati', 'Siswati')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'sk', 'Slovak', 'Slowakisch', 'Slovaque', 'Slovacco', 'Eslovaco')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'sl', 'Slovenian', 'Slowenisch', 'En slov�ne', 'Sloveno', 'Esloveno')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'so', 'Somali', 'Somalisch', 'Somali', 'Somalo', 'Somal�')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'es', 'Spanish', 'Spanisch', 'En espagnol', 'Spagnolo', 'Espa�ol')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'su', 'Sudanese', 'Sudanesisch', 'Soudanais', 'Sudanese', 'Sudan�s')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'sw', 'Swahili', 'Suaheli', 'Swahili', 'Swahili', 'Swahili')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'sv', 'Swedish', 'Schwedisch', 'Su�dois', 'Svedese', 'Sueco')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'tl', 'Tagalog', 'Tagalog', 'Tagalog', 'Tagalog', 'Tagalo')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'tg', 'Tajik', 'Tadschikisch', 'Tadjik', 'Tajik', 'Tayikist�n')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'ta', 'Tamil', 'Tamilisch', 'Tamil', 'Tamil', 'Tamil')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'tt', 'Tatar', 'Tatarisch', 'Tatar', 'Tatar', 'T�rtaro')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'te', 'Tegulu', 'Tegulu', 'Tegulu', 'Tegulu', 'Tegulu')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'th', 'Thai', 'Thail�ndisch', 'Tha�landais', 'Thai', 'Tailand�s')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'bo', 'Tibetan', 'Tibetisch', 'Tib�tain', 'Tibetano', 'Tibetano')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'ti', 'Tigrinya', 'Tigrinya', 'Tigrinya', 'Tigrinya', 'Tigrinya')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'to', 'Tonga', 'Tonga', 'Tonga', 'Tonga', 'Tonga')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'ts', 'Tsonga', 'Tsonga', 'Tsonga', 'Tsonga', 'Tsonga')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'tr', 'Turkish', 'T�rkisch', 'En turc', 'Turco', 'Turco')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'tk', 'Turkmen', 'Turkmenisch', 'Turkm�ne', 'Turkmeno', 'Turkmenist�n')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'tw', 'Twi', 'Twi', 'Twi', 'Twi', 'Twi')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'uk', 'Ukrainian', 'Ukrainisch', 'Ukrainien', 'ucraino', 'Ucraniano')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'ur', 'Urdu', 'Urdu', 'Urdu', 'Urdu', 'Urdu')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'uz', 'Uzbek', 'Usbekisch', 'Ouzb�kistan', 'Uzbeko', 'Uzbekist�n')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'vi', 'Vietnamese', 'Vietnamesisch', 'Vietnamien', 'Vietnamita', 'Vietnamita')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'vo', 'Volapuk', 'Volapuk', 'Volapuk', 'Volapuk', 'Volapuk')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'cy', 'Welsh', 'Walisisch', 'Gallois', 'Gallese', 'Gal�s')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'wo', 'Wolof', 'Wolof', 'Wolof', 'Wolof', 'Wolof')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'xh', 'Xhosa', 'Xhosa', 'Xhosa', 'Xhosa', 'Xhosa')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'ji', 'Yiddish', 'Jiddisch', 'Yiddish', 'Yiddish', 'Yiddish')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'yo', 'Yoruba', 'Yoruba', 'Yoruba', 'Yoruba', 'Yoruba')
INSERT INTO [Language] ([LanguageID], [LanguageCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 'zu', 'Zulu', 'Zulu', 'Zulu', 'Zulu', 'Zul�')

INSERT INTO [Month] ([MonthID], [MonthCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 1, 'January', 'Januar', 'Janvier', 'Gennaio', 'Enero');
INSERT INTO [Month] ([MonthID], [MonthCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 2, 'February', 'Februar', 'F�vrier', 'Febbraio', 'Febrero');
INSERT INTO [Month] ([MonthID], [MonthCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 3, 'March', 'M�rz', 'Mars', 'Marzo', 'March');
INSERT INTO [Month] ([MonthID], [MonthCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 4, 'April', 'April', 'Avril', 'Aprile', 'Abril');
INSERT INTO [Month] ([MonthID], [MonthCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 5, 'May', 'Mai', 'Mai', 'Maggio', 'May');
INSERT INTO [Month] ([MonthID], [MonthCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 6, 'June', 'Juni', 'Juin', 'Giugno', 'Junio');
INSERT INTO [Month] ([MonthID], [MonthCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 7, 'July', 'Juli', 'Juillet', 'Luglio', 'Julio');
INSERT INTO [Month] ([MonthID], [MonthCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 8, 'August', 'August', 'Ao�t', 'Agosto', 'August');
INSERT INTO [Month] ([MonthID], [MonthCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 9, 'September', 'September', 'Septembre', 'Settembre', 'Septiembre');
INSERT INTO [Month] ([MonthID], [MonthCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 10, 'October', 'Oktober', 'Octobre', 'Ottobre', 'Octubre');
INSERT INTO [Month] ([MonthID], [MonthCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 11, 'November', 'November', 'Novembre', 'Novembre', 'Noviembre');
INSERT INTO [Month] ([MonthID], [MonthCode], [Name], [Name_de], [Name_fr], [Name_it], [Name_es]) VALUES (NEWID(), 12, 'December', 'Dezember', 'D�cembre', 'Dicembre', 'Diciembre');

INSERT INTO [About]
VALUES(NEWID(), '','"Great things in business are never done by one person. They�re done by a team of people." Steve Jobs',  null)

INSERT INTO [Person]
VALUES (NEWID(), 'Alexandros', 'Theodoracatos', '1983-06-23', 1, 'Zwischenb�chen', 143, 8048, 'Z�rich', 'Z�rich', 'theodoracatos@gmail.com', '787044438',  (SELECT TOP 1 [CountryID] FROM [Country]), (SELECT TOP 1 [AboutID] FROM [About]),  (SELECT TOP 1 [LanguageID] FROM [Language]))

INSERT INTO [Curriculum]
VALUES (NEWID(), 'a05c13a8-21fb-42c9-a5bc-98b7d94f464a', 'theodoracatos', null, GETDATE(), GETDATE(), (SELECT TOP 1 [PersonID] FROM [Person]))


INSERT INTO [SocialLink]
VALUES (NEWID(), 1, 'https://www.facebook.com/theodoracatos', 1, (SELECT TOP 1 [PersonID] FROM [Person]))

INSERT INTO [SocialLink]
VALUES (NEWID(), 2, 'https://twitter.com/theodoracatos', 2, (SELECT TOP 1 [PersonID] FROM [Person]))

INSERT INTO [SocialLink]
VALUES (NEWID(), 3, 'https://www.linkedin.com/in/theodoracatos', 3, (SELECT TOP 1 [PersonID] FROM [Person]))

INSERT INTO [SocialLink]
VALUES (NEWID(), 4, 'https://www.xing.com/profile/Alexandros_Theodoracatos/cv', 4, (SELECT TOP 1 [PersonID] FROM [Person]))


INSERT INTO [Experience]
VALUES (NEWID(), 'Senior Softwareingenieur', 'Quilvest (Switzerland) Ltd.', 'http://quilvest.com', 'Z�rich', 'Architektur und Fullstack-Entwicklung elektronischer Businessprozesse, realisiert mit neuesten .NET Web-Technologien. Technischer Lead folgender eigenentwickelter Applikationssysteme: Intranet, eBanking, MBO-System, Client-OnBoard-Dokumentenerstellung. Projektleitung mit fachlicher F�hrung von 5 � 7 Mitarbeitern (l�nder�bergreifend). Regelm�ssige Schulung und Coaching von Mitarbeitern (auf Deutsch und Englisch)', '2011-02-01', null, 1, (SELECT [CountryID] FROM [Country] ORDER BY [CountryID] OFFSET 1 ROWS FETCH FIRST 1 ROW ONLY), (SELECT TOP 1 [PersonID] FROM [Person]))

INSERT INTO [Experience]
VALUES (NEWID(), 'Software Ingenieur', 'Ruf Telematik AG', 'http://ruf.ch', 'Schlieren', 'Architektur, Spezifikation, Design, Implementation, Test und Dokumentation von Softwarekomponenten und Multimediaapplikationen. Projektarbeit: Anforderungsspezifikation, Umsetzung, Engineering, Test und Projektleitung mit direktem Kundenkontakt', '2008-11-01', '2011-01-31', 2, (SELECT [CountryID] FROM [Country] ORDER BY [CountryID] OFFSET 2 ROWS FETCH FIRST 1 ROW ONLY), (SELECT TOP 1 [PersonID] FROM [Person]))

INSERT INTO [Experience]
VALUES (NEWID(), 'Freelancer', 'Ruf Telematik AG', 'http://ruf.ch', 'Schlieren', 'Entwicklung von Tools und Diagnoseprogrammen f�r Embedded-Ger�te', '2005-09-01', '2008-10-01', 3, (SELECT [CountryID] FROM [Country] ORDER BY [CountryID] OFFSET 3 ROWS FETCH FIRST 1 ROW ONLY), (SELECT TOP 1 [PersonID] FROM [Person]))

INSERT INTO [Experience]
VALUES (NEWID(), 'Trainee', 'ABB Schweiz AG', 'http://abb.ch', 'Z�rich', 'Entwicklung von Systemtools f�r elektrische Schaltanlagen (IEC 61850). Entwicklung von diversen Multimediaapplikationen f�r Firmenpr�sentationen (Demos)', '2003-08-01', '2005-08-01', 4, (SELECT [CountryID] FROM [Country] ORDER BY [CountryID] OFFSET 4 ROWS FETCH FIRST 1 ROW ONLY), (SELECT TOP 1 [PersonID] FROM [Person]))


INSERT INTO [Education]
VALUES (NEWID(), 'ZHAW School of Engineering', 'https://zhaw.ch', 'Winterthur', 'Master of Advances Studies (MAS)', 'Wirtschaftsinformatik',  'Berufsbegleitendes Nachdiplomstudium mit Schwerpunkten in: Betriebswirtschaft, Software Engineering, Projektmanagement und Coaching', '5.4', '2010-02-01', '2011-11-01', 1, (SELECT [CountryID] FROM [Country] ORDER BY [CountryID] OFFSET 1 ROWS FETCH FIRST 1 ROW ONLY), (SELECT TOP 1 [PersonID] FROM [Person]))

INSERT INTO [Education]
VALUES (NEWID(), 'ZHAW School of Engineering', 'https://zhaw.ch', 'Winterthur', 'Diplomstudium (Dipl. Ing. FH)', 'Informationstechnologie', 'Vollzeit Diplomstudium mit Schwerpunkten in: Software Architekturen / Modellierung und Softwareentwicklung', '5.3', '2005-09-01', '2008-10-01', 2, (SELECT [CountryID] FROM [Country] ORDER BY [CountryID] OFFSET 2 ROWS FETCH FIRST 1 ROW ONLY), (SELECT TOP 1 [PersonID] FROM [Person]))

INSERT INTO [Education]
VALUES (NEWID(), 'GIBB', 'https://gibb.ch', 'Bern', 'Fachausweis (FA)', 'Applikationsentwicklung', 'Die Lehre zum Applikationsentwickler f�r Maturanden (way-up.ch) bot eine zweij�hrige Praxiserfahrung in verschiedenen Firmen und ebnete den Weg zur Fachhochschule', '5.5', '2003-08-01', '2005-08-01', 3, (SELECT [CountryID] FROM [Country] ORDER BY [CountryID] OFFSET 3 ROWS FETCH FIRST 1 ROW ONLY), (SELECT TOP 1 [PersonID] FROM [Person]))

INSERT INTO [Education]
VALUES (NEWID(), 'Kantonsschule SH', 'https://kanti.ch', 'Schaffhausen', 'Matura', 'Naturwissenschaften (Profil N)', 'Naturwissenschaftlich�mathematischeMatura mit Schwerpunkten in Chemie / Biologie', '4.5', '1998-08-01', '2002-07-01', 4, (SELECT [CountryID] FROM [Country] ORDER BY [CountryID] OFFSET 4 ROWS FETCH FIRST 1 ROW ONLY), (SELECT TOP 1 [PersonID] FROM [Person]))


INSERT INTO [LanguageSkill] 
VALUES(NEWID(), 4, 1, (SELECT [LanguageID] FROM [Language] ORDER BY [LanguageID] OFFSET 1 ROWS FETCH FIRST 1 ROW ONLY), (SELECT TOP 1 [PersonID] FROM [Person]))

INSERT INTO [LanguageSkill] 
VALUES(NEWID(), 3, 2, (SELECT [LanguageID] FROM [Language] ORDER BY [LanguageID] OFFSET 2 ROWS FETCH FIRST 1 ROW ONLY), (SELECT TOP 1 [PersonID] FROM [Person]))

INSERT INTO [LanguageSkill] 
VALUES(NEWID(), 2, 3, (SELECT [LanguageID] FROM [Language] ORDER BY [LanguageID] OFFSET 3 ROWS FETCH FIRST 1 ROW ONLY), (SELECT TOP 1 [PersonID] FROM [Person]))

INSERT INTO [LanguageSkill] 
VALUES(NEWID(), 1, 4, (SELECT [LanguageID] FROM [Language] ORDER BY [LanguageID] OFFSET 4 ROWS FETCH FIRST 1 ROW ONLY), (SELECT TOP 1 [PersonID] FROM [Person]))

INSERT INTO [Award]
VALUES(NEWID(), 'ZHAW Alumni Award', 'Best award ever from ZHAW', 'Z�rcher Hochschule f�r Angewandte Wissenschaften (ZHAW)', 'www.zhaw.ch', '2012-01-01', 1,  (SELECT TOP 1 [PersonID] FROM [Person]))

INSERT INTO [Award]
VALUES(NEWID(), 'ZHAW Alumni Award', 'Secend award from ZHAW', 'Z�rcher Hochschule f�r Angewandte Wissenschaften (ZHAW)', 'www.zhaw.ch', '2011-01-01', 2,  (SELECT TOP 1 [PersonID] FROM [Person]))


INSERT INTO [Interest]
VALUES(NEWID(), 'Handball', 'Kadetten Schaffhausen', 'https://kadettensh.ch', 1, (SELECT TOP 1 [PersonID] FROM [Person]))


-------------------

--select * from [Person]
--select * from [About]
--select * from [Vfile]
--select * from [Curriculum]
--select * from [SocialLink]
--select * from [Experience]
--select * from [Education]
--select * from [Language]
--select * from [LanguageSkill]

--delete [About]
--DBCC CHECKIDENT ('[About]', RESEED, 0);
--GO

--delete [Person]
--DBCC CHECKIDENT ('[Person]', RESEED, 0);
--GO

--delete [Curriculum]
--DBCC CHECKIDENT ('[Curriculum]', RESEED, 0);
--GO

--delete [SocialLink]
--DBCC CHECKIDENT ('[Curriculum]', RESEED, 0);
--GO

--------------------------------


COMMIT TRANSACTION -- Transaction Success!
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        RAISERROR (50001,1,1)
        ROLLBACK TRAN --RollBack in case of Error
END CATCH