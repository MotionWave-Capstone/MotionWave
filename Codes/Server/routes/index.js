var express = require('express');
var router = express.Router();

var controller = require('../controller/database.js')

/**
 * @swagger
 * /domain/cart_list:
 *   get:
 *     summary: Retrieve the list of cars
 *     description: Fetches a list of all cars from the database.
 *     responses:
 *       200:
 *         description: A JSON array of car objects.
 *         content:
 *           application/json:
 *             schema:
 *               type: array
 *               items:
 *                 type: object
 *                 properties:
 *                   inum:
 *                     type: integer
 *                     description: The car ID.
 *                   generation:
 *                     type: string
 *                     description: The car generation model.
 *                   brand:
 *                     type: string
 *                     description: The car brand.
 *                   prdate:
 *                     type: string
 *                     description: The production date of the car.
 */

/**
 * @swagger
 * /domain/car_data:
 *   get:
 *     summary: Retrieve car data by index
 *     description: Fetches detailed data about a specific car using its index.
 *     parameters:
 *       - in: query
 *         name: index
 *         required: true
 *         description: The index of the car to retrieve data for.
 *         schema:
 *           type: integer
 *     responses:
 *       200:
 *         description: A JSON object containing car data.
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 inum:
 *                   type: integer
 *                   description: The car ID.
 *                 generation:
 *                   type: string
 *                   description: The car generation model.
 *                 brand:
 *                   type: string
 *                   description: The car brand.
 *                 prdate:
 *                   type: string
 *                   description: The production date of the car.
 */


/* GET home page. */
// 기본 구성
router.get('/cart_list', function(req, res, next) {
  //res.render('index', { title: 'Express' });
  console.log('/domain/cart_list check');
  controller.carList()
    .then((val) => {
      res.send(val);
    }, (err) => {
      res.send(err);
    })
});

router.get('/car_data', function(req, res, next){
  console.log('/domain/car_data check');
  const inum = req.query.index;
  if (!inum) {
    return res.status(400).send({error : "index error"})
  }
  controller.carData(inum)
    .then((val) => {
      res.send(val);
    }, (err) => {
      res.send(err);
    })
})

module.exports = router;
