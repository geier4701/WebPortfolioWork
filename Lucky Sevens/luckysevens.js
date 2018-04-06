function validateItems() {
	var fullBet = document.forms["totalBet"]["startingBet"].value;

	if (fullBet == "" || isNaN(fullBet) || fullBet <= 0) {
		alert("Starting bet must be a number greater than zero");
		return false;
	}

	document.getElementById("submitButton").innerText = "Play Again";
	document.getElementById("betTable1").innerText = fullBet;
	rollDice(fullBet);
	document.getElementById("fullTable").style.display = "block"

	return false;
}

function rollDice(fullBet) {
	var totalRolls = 0;
	var highestAmount = fullBet;
	var rollCount = 0;

	do {
		totalRolls++;

		var roll1 = Math.ceil(Math.random() * (1 + 6 -1));
		var roll2 = Math.ceil(Math.random() * (1 + 6 -1));
		var total = roll1 + roll2;

		if (total == 7) {
			fullBet = fullBet + 4;
		} else {
			fullBet--;
		}

		if (fullBet > highestAmount) {
			highestAmount = fullBet;
			rollCount = totalRolls;
		}

	} while (fullBet > 0);

	document.getElementById("totalRolls").innerText = totalRolls;
	document.getElementById("highestAmount").innerText = highestAmount;
	document.getElementById("rollCount").innerText = rollCount;
}