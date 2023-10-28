using ScrumPoker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrumPoker.Store;

public record StartGameAction(string Story);

public record OnPlaceBetAction(PokerPlayer Player, int Bet);
