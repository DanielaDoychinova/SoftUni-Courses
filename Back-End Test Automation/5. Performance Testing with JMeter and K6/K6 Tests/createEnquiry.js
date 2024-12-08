import http from 'k6/http';
import { check, sleep } from 'k6';

export const options = {
    stages: [
        { duration: '1m', target: 10},
    ]
};

export default function (){
const payload = JSON.stringify({
    name: 'Some Body',
    email: 'test@email.com',
    mobile: '+123456789',
    comment: 'Some comment here.',
    status: 'In Progress'
});

const params = {
    headers: {
        'Content-Type': 'application/json',
    },
};

    const res = http.post('http://localhost:5000/api/enquiry/', payload, params);
    check(res, {
        'status code is 200': (r) => r.status === 200,
        'contains expected fields': (r) => {
            const body = JSON.parse(r.body);
            return body.hasOwnProperty('_id') && body.hasOwnProperty('status')
        }
    });
    sleep(1)
}