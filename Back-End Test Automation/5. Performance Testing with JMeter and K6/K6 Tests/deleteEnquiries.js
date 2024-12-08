import http from 'k6/http';
import { check, sleep } from 'k6';

export const options = {
    stages: [
        { duration: '1m', target: 10},
    ]
};

export default function (){

    const adminToken = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjY3NGY0NTc2Yzk2ZDFjOGY2NWRjNGY0OCIsImlhdCI6MTczMzI1MDc3MCwiZXhwIjoxNzMzMzM3MTcwfQ.HJaBJWw2Ng_PCblSKD1A8SBLV240vvm11baDzY71dYQ';

    const params = {
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${adminToken}`,
        },
    };

    const resGet = http.get('http://localhost:5000/api/enquiry/');

    const enquiries = JSON.parse(resGet.body);

    enquiries.forEach((enquiry) => {
        if (enquiry.name === 'Some Body') {
            const deleteRes = http.del(`http://localhost:5000/api/enquiry/${enquiry._id}`, null, params);

            check(deleteRes, {
                'Delete response has crrect status code': (r) => r.status === 200
            })
        }
    });

    sleep(1)
}