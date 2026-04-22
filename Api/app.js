import express from 'express';
import db from './db.js';
const PORT = 2000;

const app = express();

app.use(express.json());

app.get('/', (req, res) => {
  const result = db.prepare('SELECT * FROM PremiumMembership').all();
  res.status(200).json(result);
});
app.get('/:id', (req, res) => {
  const { id } = +req.params;
  const result = db.prepare('SELECT * FROM PremiumMembership WHERE id = ?').get(id);
  res.status(200).json(result);
});

app.post('/', (req, res) => {
  const { GameName, OrderDate, ExpirationDate, Price } = req.body;
  const result = db.prepare('INSERT INTO PremiumMembership (GameName, OrderDate, ExpirationDate, Price) VALUES (?, ?, ?, ?)').run(GameName, OrderDate, ExpirationDate, Price);
  res.status(201).json({ id: result.lastInsertRowid, GameName, OrderDate, ExpirationDate, Price });
});
app.put('/:id', (req, res) => {
  const { id } = +req.params;
  const { GameName, OrderDate, ExpirationDate, Price } = req.body;
  const result = db.prepare('UPDATE PremiumMembership SET GameName = ?, OrderDate = ?, ExpirationDate = ?, Price = ? WHERE id = ?').run(GameName, OrderDate, ExpirationDate, Price, id);
  if (result.changes === 0) {
    res.status(404).json({ error: 'PremiumMembership not found' });
  } else {
    res.status(200).json({ id, GameName, OrderDate, ExpirationDate, Price });
  }
});
app.delete('/:id', (req, res) => {
  const { id } = +req.params;
  const result = db.prepare('DELETE FROM PremiumMembership WHERE id = ?').run(id);
  if (result.changes === 0) {
    res.status(404).json({ error: 'PremiumMembership not found' });
  } else {
    res.status(204).send();
  }
});

app.listen(PORT, () => {
  console.log(`Server is running on http://localhost:${PORT}`);
});