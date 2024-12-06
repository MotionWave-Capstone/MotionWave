// npm install mariadb
// https://reddb.tistory.com/143

const mariadb = require('mariadb');

const pool = mariadb.createPool(
    {
        host: 'localhost',
        port: 3306,
        user: 'root',
        password: '!123456',
        database: 'motion_wave'
    }
);

async function carList() {
    let conn;
    try {
        // 커넥션 가져오기
        conn = await pool.getConnection();
        // 쿼리 실행
        const result = await conn.query("SELECT * FROM carInfo");
        // 결과 반환
        return result;
    } catch (err) {
        console.error("Error in carList:", err);
        throw err;
    } finally {
        // 연결 반환
        if (conn) conn.release();
    }
}

async function carData(inum) {
    let conn;
    try {
        conn = await pool.getConnection();
        const result = await conn.query("SELECT * FROM carData WHERE inum = ?", [inum]);
        return result;
    } catch (err) {
        console.error("Error in carData:", err);
        throw err;
    } finally {
        if (conn) conn.release();
    }
}

module.exports = {
    carList: carList,
    carData: carData
}