2026-06-07: 
	Ordnerstruktur aufgebaut, 
	Initialen Git-Commit, 
	erstes Movement des Player-Charakters, 
	Coyote Time hinzugefügt,
	Attack Hitbox hinzugefügt mit Angriffs-State Timer,
	TestEnemy mit Gesundheit und verbunden mit Attack Hitbox Signal.
2026-06-08:
	QueueFree() Methode bei Tod des Gegners aufgerufen.
	Bugfix DoubleJump
2026-06-09:
	2D-Kamera an Charakter gebunden
2026-06-10:
	Sprite Flip hinzugefügt
	Attack Hitbox ändert Position bei Richtungswechsel
	.gitignore geändert
	Todesanimation für Gegner hinzugefügt.
2026-06-11:
	Idle Animation für Enemy
	Health Bar für Player
	PlayerData Singleton, um Daten global zu laden
	GameManager, um Spielgeschehen zu verarbeiten
	Mehrere Änderungen zur Optimierung auf vorherige Änderungen
2026-06-12:
	Paralax2D für dynamischen Hintergrund hinzugefügt.
2026-06-13:
	HUD.cs erstellt und mit HealthBar verbunden
	AttackHitbox für Enemy erstellt.
2026-06-17:
	Attack Logik für TestEnemy erstellt.
	Schnelle Bewegung, wenn man Strg gedrückt hält.
2026-06-20:
	Attack Logik mit korrekter Signal Chain erstellt und debugged.
2026-06-22:
	Angfang eines Refactorings für OOP Ansatz
2026-06-23:
	Label für Textdarstellung testweise erstellt,
	für Damage Darstellung des Gegners.
2026-06-24:
	Refactoring der Schadenskette
2026-06-25:
	Refactoring der Schadenskette mit OOP Architektur
2026-06-26:
	Randomized Damage des Players für mehr Varianz, auf Basis der Grundstärke
2026-06-29:
	Box-Muller-Tranform für Normalverteillung des Damages eingebaut.
2026-07-01:
	CritChance Logik eingebaut und mit EmitSignal versendet.
2026-07-03:
	Funktionierende Health Logik des Player und Print, wenn Spieler stirbt.
	Bool in TakeDamage, damit Todes-Signal nur 1x feuer bei Tod.
	Game Over Screen mit Szenenwechsel bei Tod.
	Start Screen mit funktionieren Buttons und spielernder Titelmusik.
